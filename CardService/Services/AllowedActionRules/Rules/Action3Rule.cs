using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action3Rule : ICardActionRule
{
    public string Action=> "ACTION3";
    public bool IsAllowed(CardDetails details) => true;
}