using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public interface ICardActionRule
{
    CardAction Action { get; }
    bool IsAllowed(CardDetails details);
}