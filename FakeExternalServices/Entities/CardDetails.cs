namespace FakeExternalServices.Entities;

public record CardDetails
{
    public string CardNumber { get; set; }
    public CardType CardType { get; set; }
    public CardStatus CardStatus { get; set; }
    public bool IsPinSet { get; set; }

    public CardDetails(string cardNumber, CardType cardType, CardStatus cardStatus, bool isPinSet)
    {
        CardNumber = cardNumber;
        CardType = cardType;
        CardStatus = cardStatus;
        IsPinSet = isPinSet;
    }
}