using CardService.Entities;
using CardService.Infrastructure.HttpClients;
using CardService.Services.AllowedActionRules;
using CardService.UserCardsModule.Queries;
using MediatR;

namespace CardService.UserCardsModule.Handlers;

public class CardActionsQueryHandler : IRequestHandler<CardActionsQuery, IEnumerable<string>>
{
    private readonly ICardDetailsClient _cardDetailsClient;
    private readonly ICardAllowedActionsChooser _cardAllowedActionsChooser;

    public CardActionsQueryHandler(ICardDetailsClient cardDetailsClient,
        ICardAllowedActionsChooser cardAllowedActionsChooser)
    {
        _cardDetailsClient = cardDetailsClient;
        _cardAllowedActionsChooser = cardAllowedActionsChooser;
    }

    public async Task<IEnumerable<string>> Handle(CardActionsQuery request, CancellationToken cancellationToken)
    {
        var cardDetails = await _cardDetailsClient.GetCardDetailsAsync(request.UserId, request.CardNumber);
        if (cardDetails == null) 
            throw new KeyNotFoundException();
        return _cardAllowedActionsChooser.Get(cardDetails);
    }
}