using CardService.Entities;
using CardService.Infrastructure.HttpClients;
using CardService.Services.AllowedActionRules;

namespace CardService.Services;

public class CardsService : ICardsService
{
    private readonly ICardDetailsClient _cardDetailsClient;
    private readonly ICardAllowedActionsChooser _cardAllowedActionsChooser;

    //private static List<Product> _productsRepository =
    //[
    //    new() { Id = 1, Name = "Product 1" },
    //    new() { Id = 2, Name = "Product 2" },
    //];

    public CardsService(ICardDetailsClient cardDetailsClient,
        ICardAllowedActionsChooser cardAllowedActionsChooser)
    {
        _cardDetailsClient = cardDetailsClient ?? throw new ArgumentNullException(nameof(cardDetailsClient));
        _cardAllowedActionsChooser = cardAllowedActionsChooser;
    }

    public async Task<List<CardAction>> GetPossibleActionsForUserCard(string user, string cardNumber)
    {
        var cardDetails = await _cardDetailsClient.GetCardDetailsAsync(user, cardNumber);
        return this._cardAllowedActionsChooser.Get(cardDetails);
    }

    //public Product? GetProduct(int productId) => _productsRepository.Find(p => p.Id == productId);

    //public void CreateProduct(Product product)
    //{
    //    product.Id = _productsRepository.Max(p => p.Id) + 1;
    //    _productsRepository.Add(product);
    //}

    //public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
    //{
    //    // At this point, we would typically make an HTTP call to an external service
    //    // to fetch the data. For this example we use generated sample data.
    //    await Task.Delay(1000);
    //    //if (!_userCards.TryGetValue(userId, out var cards)
    //    //    || !cards.TryGetValue(cardNumber, out var cardDetails))
    //    //{
    //    //    return null;
    //    //}
    //    //return cardDetails
    //    return null;
    //}
}

public interface ICardsService
{
    Task<List<CardAction>> GetPossibleActionsForUserCard(string user, string cardNumber);
}
