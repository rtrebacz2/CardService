using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class CardAllowedActionsChooser : ICardAllowedActionsChooser
{
    private readonly IEnumerable<ICardActionRule> _rules;

    public CardAllowedActionsChooser(IEnumerable<ICardActionRule> rules)
    {
        _rules = rules;
    }
    public List<CardAction> Get(CardDetails? cardDetails)
    {
        if (cardDetails == null)
            return new List<CardAction>();

        var results = _rules
            .Where(r => r.IsAllowed(cardDetails))
            .Select(r => r.Action)
            .ToList();
        return results;
    }
}

public interface ICardAllowedActionsChooser
{
    List<CardAction> Get(CardDetails? cardDetails);
}