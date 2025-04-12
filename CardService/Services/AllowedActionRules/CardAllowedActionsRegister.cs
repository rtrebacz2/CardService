namespace CardService.Services.AllowedActionRules;

public static class CardAllowedActionsRegister
{
    public static void RegisterCardAllowedActions(this IServiceCollection services)
    {
        services.AddScoped<ICardActionRule, Action1Rule>();
        services.AddScoped<ICardActionRule, Action2Rule>();
        services.AddScoped<ICardActionRule, Action3Rule>();
        services.AddScoped<ICardActionRule, Action4Rule>();
        services.AddScoped<ICardActionRule, Action5Rule>();
        services.AddScoped<ICardActionRule, Action6Rule>();
        services.AddScoped<ICardActionRule, Action7Rule>();
        services.AddScoped<ICardActionRule, Action8Rule>();
        services.AddScoped<ICardActionRule, Action9Rule>();
        services.AddScoped<ICardActionRule, Action10Rule>();
        services.AddScoped<ICardActionRule, Action11Rule>();
        services.AddScoped<ICardActionRule, Action12Rule>();
        services.AddScoped<ICardActionRule, Action13Rule>();
    }
}