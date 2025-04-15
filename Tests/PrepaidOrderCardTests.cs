using CardService.Entities;
using CardService.Services.AllowedActionRules;
using CardService.Services.AllowedActionRules.Rules;

namespace Tests;

public class PrepaidCardTests
{
    private readonly IEnumerable<ICardActionRule> _rules;
    private readonly CardAllowedActionsChooser _cardAllowedActionsChooser;

    public PrepaidCardTests()
    {
        _rules = [
            new Action1Rule(),
            new Action2Rule(),
            new Action3Rule(),
            new Action4Rule(),
            new Action5Rule(),
            new Action6Rule(),
            new Action7Rule(),
            new Action8Rule(),
            new Action9Rule(),
            new Action10Rule(),
            new Action11Rule(),
            new Action12Rule(),
            new Action13Rule()
            ];
        _cardAllowedActionsChooser = new CardAllowedActionsChooser(_rules);
    }

    [Test]
    [TestCaseSource(nameof(Test))]
    public void GetAllowedActionsForCard((CardType Type, CardStatus Status, bool IsPinSet, IEnumerable<string> expected) testCase)
    {
        var card = CreatePrepaidCard(testCase.Type, testCase.Status, testCase.IsPinSet);

        var result = _cardAllowedActionsChooser.Get(card);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EquivalentTo(testCase.expected));
    }

    private static CardDetails CreatePrepaidCard(CardType type, CardStatus status, bool isPinSet) => new()
    {
        CardNumber = "Card11",
        CardType = type,
        CardStatus = status,
        IsPinSet = isPinSet
    };

    private static IEnumerable<(CardType Type, CardStatus Status, bool IsPinSet, IEnumerable<string>)> Test()
    {
        foreach(var cardWithActionResult in _cardWithActionResults)
        {
            yield return cardWithActionResult;
        }
    }

    private static readonly List<(CardType Type, CardStatus Status, bool IsPinSet, IEnumerable<string> exptectedResult)> _cardWithActionResults = new()
    {
        // PREPAID
        {(CardType.PREPAID, CardStatus.ORDERED, true, ["ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"])},
        {(CardType.PREPAID, CardStatus.ORDERED, false, ["ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"])},
        {(CardType.PREPAID, CardStatus.INACTIVE, true, ["ACTION2", "ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.PREPAID, CardStatus.INACTIVE, false, ["ACTION2", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.PREPAID, CardStatus.ACTIVE, true, ["ACTION1", "ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.PREPAID, CardStatus.ACTIVE, false, ["ACTION1", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.PREPAID, CardStatus.RESTRICTED, true, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.PREPAID, CardStatus.RESTRICTED, false, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.PREPAID, CardStatus.BLOCKED, true, ["ACTION3", "ACTION4", "ACTION6", "ACTION7", "ACTION8", "ACTION9"])},
        {(CardType.PREPAID, CardStatus.BLOCKED, false, ["ACTION3", "ACTION4", "ACTION8", "ACTION9"])},
        {(CardType.PREPAID, CardStatus.EXPIRED, true, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.PREPAID, CardStatus.EXPIRED, false, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.PREPAID, CardStatus.CLOSED, true, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.PREPAID, CardStatus.CLOSED, false, ["ACTION3", "ACTION4", "ACTION9"])},
        // DEBIT
        {(CardType.DEBIT, CardStatus.ORDERED, true, ["ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"])},
        {(CardType.DEBIT, CardStatus.ORDERED, false, ["ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"])},
        {(CardType.DEBIT, CardStatus.INACTIVE, true, ["ACTION2", "ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.DEBIT, CardStatus.INACTIVE, false, ["ACTION2", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.DEBIT, CardStatus.ACTIVE, true, ["ACTION1", "ACTION3", "ACTION4", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.DEBIT, CardStatus.ACTIVE, false, ["ACTION1", "ACTION3", "ACTION4", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.DEBIT, CardStatus.RESTRICTED, true, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.DEBIT, CardStatus.RESTRICTED, false, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.DEBIT, CardStatus.BLOCKED, true, ["ACTION3", "ACTION4", "ACTION6", "ACTION7", "ACTION8", "ACTION9"])},
        {(CardType.DEBIT, CardStatus.BLOCKED, false, ["ACTION3", "ACTION4", "ACTION8", "ACTION9"])},
        {(CardType.DEBIT, CardStatus.EXPIRED, true, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.DEBIT, CardStatus.EXPIRED, false, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.DEBIT, CardStatus.CLOSED, true, ["ACTION3", "ACTION4", "ACTION9"])},
        {(CardType.DEBIT, CardStatus.CLOSED, false, ["ACTION3", "ACTION4", "ACTION9"])},
        // CREDIT
        {(CardType.CREDIT, CardStatus.ORDERED, true, ["ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"])},
        {(CardType.CREDIT, CardStatus.ORDERED, false, ["ACTION3", "ACTION4", "ACTION5", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION12", "ACTION13"])},
        {(CardType.CREDIT, CardStatus.INACTIVE, true, ["ACTION2", "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.CREDIT, CardStatus.INACTIVE, false, ["ACTION2", "ACTION3", "ACTION4", "ACTION5", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.CREDIT, CardStatus.ACTIVE, true, ["ACTION1", "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.CREDIT, CardStatus.ACTIVE, false, ["ACTION1", "ACTION3", "ACTION4", "ACTION5", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13"])},
        {(CardType.CREDIT, CardStatus.RESTRICTED, true, ["ACTION3", "ACTION4", "ACTION5", "ACTION9"])},
        {(CardType.CREDIT, CardStatus.RESTRICTED, false, ["ACTION3", "ACTION4", "ACTION5", "ACTION9"])},
        {(CardType.CREDIT, CardStatus.BLOCKED, true, ["ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9"])},
        {(CardType.CREDIT, CardStatus.BLOCKED, false, ["ACTION3", "ACTION4", "ACTION5", "ACTION8", "ACTION9"])},
        {(CardType.CREDIT, CardStatus.EXPIRED, true, ["ACTION3", "ACTION4", "ACTION5", "ACTION9"])},
        {(CardType.CREDIT, CardStatus.EXPIRED, false, ["ACTION3", "ACTION4", "ACTION5", "ACTION9"])},
        {(CardType.CREDIT, CardStatus.CLOSED, true, ["ACTION3", "ACTION4", "ACTION5", "ACTION9"])},
        {(CardType.CREDIT, CardStatus.CLOSED, false, ["ACTION3", "ACTION4", "ACTION5", "ACTION9"])},
    };
}