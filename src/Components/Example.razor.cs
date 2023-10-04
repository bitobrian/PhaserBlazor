using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChevyBlazor.Components;

public partial class Example : ComponentBase, IAsyncDisposable
{

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;

    private string _message = "Hello World!";
    private Lazy<IJSObjectReference> ExampleModule = new();
    private static readonly string[] args = new [] { "./Components/Example.razor.js"};

    private void UpdateMessage()
    {
        _message = "Hello Blazor!";
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var task = JSRuntime.InvokeAsync<IJSObjectReference>("import", args);
            ExampleModule = new(await task);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (ExampleModule.IsValueCreated)
        {
            await ExampleModule.Value.DisposeAsync();
        }
    }
}