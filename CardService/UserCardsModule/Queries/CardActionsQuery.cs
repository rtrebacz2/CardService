namespace CardService.UserCardsModule.Queries;

public record CardActionsQuery : IRequest<IEnumerable<string>>
{
    public string UserId { get; set; }

    public string CardNumber { get; set; }
}