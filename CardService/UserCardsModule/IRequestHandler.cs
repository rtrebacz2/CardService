namespace CardService.UserCardsModule;

public interface IRequestHandler<Treq, TResp>
{
    Task<TResp> Handle(Treq request, CancellationToken cancellationToken);
}