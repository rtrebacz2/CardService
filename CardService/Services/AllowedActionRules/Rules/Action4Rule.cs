using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action4Rule : ICardActionRule
{
    public string Action=> "ACTION4";
    public bool IsAllowed(CardDetails details) => true;
}