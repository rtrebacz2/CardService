using CardService.UserCardsModule.Queries;
using FluentValidation;

namespace CardService.UserCardsModule.Validators;

public class CardActionsQueryValidator : AbstractValidator<CardActionsQuery>
{
    public CardActionsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User - cannot be empty")
            .MinimumLength(5).WithMessage("User - min lenght is 5 letters")
            .MaximumLength(5).WithMessage("User - max lenght is 5 letters");
        RuleFor(x => x.CardNumber)
            .NotNull().WithMessage("Card - cannot be empty")
            .MinimumLength(5).WithMessage("Card - min lenght is 5 letters")
            .MaximumLength(7).WithMessage("Card - max lenght is 7 letters");
    }
}