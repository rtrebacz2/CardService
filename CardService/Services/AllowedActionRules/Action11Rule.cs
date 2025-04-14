using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action11Rule : ICardActionRule
{
    public CardAction Action => "ACTION11";
    public bool IsAllowed(CardDetails details) => new[]{CardStatus.INACTIVE, CardStatus.ACTIVE}.Contains(details.CardStatus);
}