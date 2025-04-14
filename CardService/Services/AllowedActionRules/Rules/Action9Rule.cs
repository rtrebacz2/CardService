using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action9Rule : ICardActionRule
{
    public string Action=> "ACTION9";
    public bool IsAllowed(CardDetails details) => true;
}