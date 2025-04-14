using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action2Rule : ICardActionRule
{
    public CardAction Action => "ACTION2";
    public bool IsAllowed(CardDetails details) => details.CardStatus == CardStatus.INACTIVE;
}