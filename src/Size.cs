using Microsoft.AspNetCore.Components;

namespace Wasm.Size;

public class Size : ComponentBase
{
    [Inject] private ISizeManager SizeManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await SizeManager.InitializeAsync();
    }
}