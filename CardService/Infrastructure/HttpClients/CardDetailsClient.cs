using CardService.Entities;
using CardService.Infrastructure.Extensions;

namespace CardService.Infrastructure.HttpClients;

public class CardDetailsClient : ICardDetailsClient
{
    private readonly HttpClient _httpClient;

    public CardDetailsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber)
    {
        var url = $"/CardDetails/{userId}/{cardNumber}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var cardDetails = await response.GetFromJsonAsync<CardDetails>();
            return cardDetails;
        }
        return null;
    }
}

public interface ICardDetailsClient
{
    Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber);
}