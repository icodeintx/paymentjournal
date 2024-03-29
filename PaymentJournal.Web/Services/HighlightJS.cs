using Microsoft.AspNetCore.Components;

namespace PaymentJournal.Web;

public sealed class HighlightJS : IHighlightJS
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public HighlightJS(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "/highlight.min.js").AsTask());
    }

    public async ValueTask DisposeAsync()
    {
        if (!moduleTask.IsValueCreated)
        {
            return;
        }
        var module = await moduleTask.Value;

        await module.DisposeAsync();
    }

    public async ValueTask HighlightElement(ElementReference element)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("hljs.highlightElement", element);
    }
}