namespace CardService.Entities;

public class CardAction
{
    public string _action { get; }

    public CardAction(string action)
    {
        _action = action;
    }

    public static implicit operator CardAction?(string? action)
    {
        if (action == null)
            return null;

        return new CardAction(action);
    }

    public static implicit operator string?(CardAction? cardAction)
    {
        return cardAction?._action;
    }
}