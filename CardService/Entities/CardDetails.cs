namespace CardService.Entities;

public record CardDetails
{
    public string CardNumber { get; set; }
    public CardType CardType { get; set; }
    public CardStatus CardStatus { get; set; }
    public bool IsPinSet { get; set; }
}