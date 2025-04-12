using FakeExternalServices.Entities;

namespace FakeExternalServices.Services;

public class CardsRepository : ICardsRepository
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();

    public Dictionary<string, Dictionary<string, CardDetails>> Get()
    {
        return _userCards;
    }

    private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
    {
        var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
        for (var i = 1; i <= 3; i++)
        {
            var cards = new Dictionary<string, CardDetails>();
            var cardIndex = 1;
            foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                {
                    var cardNumber = $"Card{i}{cardIndex}";
                    cards.Add(cardNumber,
                        new CardDetails(
                            cardNumber: cardNumber,
                            cardType: cardType,
                            cardStatus: cardStatus,
                            isPinSet: cardIndex % 2 == 0));
                    cardIndex++;
                }
            }
            var userId = $"User{i}";
            userCards.Add(userId, cards);
        }
        return userCards;
    }
}

public interface ICardsRepository
{
    Dictionary<string, Dictionary<string, CardDetails>> Get();
}