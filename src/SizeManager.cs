using Microsoft.JSInterop;
using Wasm.LocalStorage;

namespace Wasm.Size;

public class SizeManager(IJSRuntime jsRuntime, ILocalStorageManager localStorageManager, ISizeConfig config) : ISizeManager, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _jsReference = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
        "import", "./_content/Wasm.Size/scripts.js").AsTask());

    private EventHandler? _sizeChanged;

    public event EventHandler SizeChanged
    {
        add => _sizeChanged += value;
        remove => _sizeChanged -= value;
    }

    public SizeType CurrentSize { get; private set; }

    public async ValueTask InitializeAsync()
    {
        await UpdateCurrentSize();
        await UpdateDocumentSize();
        await NofifySizeChanged();
    }

    public async ValueTask SetUserPreferredSizeAsync(SizeType size)
    {
        if (size == CurrentSize)
            return;
        await localStorageManager.SetItemAsync(config.LocalStorageKey, size.ToString());
        await UpdateCurrentSize();
        await UpdateDocumentSize();
        await NofifySizeChanged();
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_jsReference.IsValueCreated)
        {
            var module = await _jsReference.Value;
            await module.DisposeAsync();
        }
    }
    
    private async ValueTask UpdateCurrentSize()
    {
        var userSize = await localStorageManager.GetItemAsync(config.LocalStorageKey) ?? "Default";
        SizeType size = (SizeType) Enum.Parse(typeof(SizeType), userSize, true);
        CurrentSize = size;
    }

    private async ValueTask UpdateDocumentSize()
    {
        var js = await _jsReference.Value;
        var size = CurrentSize switch
        {
            SizeType.Sm => config.SizeSm,
            SizeType.Lg => config.SizeLg,
            SizeType.Xl => config.SizeXl,
            _ => config.SizeDefault
        };
        await js.InvokeVoidAsync("setDocumentSize", size);
    }

    private ValueTask NofifySizeChanged()
    {
        _sizeChanged?.Invoke(this, EventArgs.Empty);
        return ValueTask.CompletedTask;
    }
}