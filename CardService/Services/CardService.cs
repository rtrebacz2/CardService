using CardService.Entities;
using CardService.Infrastructure.HttpClients;
using CardService.Services.AllowedActionRules;

namespace CardService.Services;

public class CardService : ICardService
{
    private readonly ICardDetailsClient _cardDetailsClient;
    private readonly ICardAllowedActionsChooser _cardAllowedActionsChooser;

    public CardService(ICardDetailsClient cardDetailsClient,
        ICardAllowedActionsChooser cardAllowedActionsChooser)
    {
        _cardDetailsClient = cardDetailsClient;
        _cardAllowedActionsChooser = cardAllowedActionsChooser;
    }

    public async Task<List<CardAction>> GetPossibleActionsForUserCard(string user, string cardNumber)
    {
        var cardDetails = await _cardDetailsClient.GetCardDetailsAsync(user, cardNumber);
        return this._cardAllowedActionsChooser.Get(cardDetails);
    }
}

public interface ICardService
{
    Task<List<CardAction>> GetPossibleActionsForUserCard(string user, string cardNumber);
}
