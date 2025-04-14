using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action1Rule : ICardActionRule
{
    public CardAction Action => "ACTION1";
    public bool IsAllowed(CardDetails details) => details.CardStatus == CardStatus.ACTIVE;
}