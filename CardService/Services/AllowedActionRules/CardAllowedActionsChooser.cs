using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class CardAllowedActionsChooser : ICardAllowedActionsChooser
{
    private readonly IEnumerable<ICardActionRule> _rules;

    public CardAllowedActionsChooser(IEnumerable<ICardActionRule> rules)
    {
        _rules = rules;
    }
    public List<string> Get(CardDetails? cardDetails)
    {
        if (cardDetails == null)
            return new List<string>();

        var results = _rules
            .Where(r => r.IsAllowed(cardDetails))
            .Select(r => r.Action)
            .ToList();
        return results;
    }
}

public interface ICardAllowedActionsChooser
{
    List<string> Get(CardDetails? cardDetails);
}