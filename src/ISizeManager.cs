namespace Wasm.Size;

public interface ISizeManager
{
    event EventHandler SizeChanged;
    SizeType CurrentSize { get; }
    ValueTask InitializeAsync();
    ValueTask SetUserPreferredSizeAsync(SizeType size);
}