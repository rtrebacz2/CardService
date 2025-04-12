using FakeExternalServices.Entities;

namespace FakeExternalServices.Services;

public class CardDetailsService : ICardDetailsService
{
    private readonly ICardsRepository _cardsRepository;

    public CardDetailsService(ICardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    
    public CardDetails? GetCardDetails(string userId, string cardNumber)
    {
        var userCards = _cardsRepository.Get();
        if (!userCards.TryGetValue(userId, out var cards)
            || !cards.TryGetValue(cardNumber, out var cardDetails))
        {
            return null;
        }
        return cardDetails;
    }

    public List<CardDetails> GetAllCardDetails(string userId)
    {
        var userCards = _cardsRepository.Get();
        return !userCards.TryGetValue(userId, out var cards) ? [] : cards.Values.ToList();
    }
}

public interface ICardDetailsService
{
    CardDetails? GetCardDetails(string userId, string cardNumber);
    List<CardDetails> GetAllCardDetails(string userId);
}
