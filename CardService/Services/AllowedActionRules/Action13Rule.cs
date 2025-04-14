using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action13Rule : ICardActionRule
{
    public CardAction Action => "ACTION13";
    public bool IsAllowed(CardDetails details) => new[]{CardStatus.INACTIVE, CardStatus.ACTIVE}.Contains(details.CardStatus);
}