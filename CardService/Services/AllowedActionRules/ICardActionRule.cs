using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public interface ICardActionRule
{
    string Action { get; }
    bool IsAllowed(CardDetails details);
}