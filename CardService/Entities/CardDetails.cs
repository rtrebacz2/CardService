namespace CardService.Entities;

public class CardDetails
{
    public string Number { get; set; }
    public CardType Type { get; set; }
    public CardStatus Status { get; set; }
    public bool IsPinSet { get; set; }
}