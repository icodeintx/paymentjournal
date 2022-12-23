using Microsoft.AspNetCore.Components;

namespace PaymentJournal.Web;

public interface IHighlightJS : IAsyncDisposable
{
    ValueTask HighlightElement(ElementReference element);
}