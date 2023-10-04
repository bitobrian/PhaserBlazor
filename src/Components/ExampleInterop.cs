using Microsoft.JSInterop;

namespace ChevyBlazor.Components;

public class ExampleJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ExampleJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./Components/Example.razor.js").AsTask());
    }

    // public async ValueTask<string> Prompt(string message)
    // {
    //     var module = await moduleTask.Value;
    //     return await module.InvokeAsync<string>("showPrompt", message);
    // }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}