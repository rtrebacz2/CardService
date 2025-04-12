using FakeExternalServices.Entities;
using FakeExternalServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeExternalServices.Controllers;

[Route("[controller]")]
public class CardDetailsController(ICardDetailsService cardDetailsService) : ControllerBase
{
    //[HttpGet]
    //public ActionResult<List<ProductResponse>> GetAll()
    //{
    //    return productsService
    //        .GetAllProducts()
    //        .Select(ProductResponse.FromProduct)
    //        .ToList();
    //}

    [HttpGet("{userId}/{cardNumber}")]
    public ActionResult<CardDetailsResponse> Get(string userId, string cardNumber)
    {
        var cardDetails = cardDetailsService.GetCardDetails(userId, cardNumber);

        return cardDetails is null
            ? Problem(detail: $"Card {cardNumber} not found.", statusCode: StatusCodes.Status404NotFound)
            : CardDetailsResponse.FromCardDetails(cardDetails);
    }

    [HttpGet("{userId}")]
    public ActionResult<List<CardDetails>> Get(string userId)
    {
        var cardDetails = cardDetailsService.GetAllCardDetails(userId);

        return cardDetails;
    }

    //[HttpPost]
    //public IActionResult Post([FromBody] CreateProductRequest createProductRequest)
    //{
    //    var product = createProductRequest.ToProduct();

    //    productsService.CreateProduct(product);

    //    return CreatedAtAction(
    //        actionName: nameof(Get),
    //        routeValues: new { productid = product.Id },
    //        value: ProductResponse.FromProduct(product));
    //}
}

//public record CreateProductRequest(string Name = "")
//{
//    public Product ToProduct()
//        => new() { Name = Name };
//}

public record CardDetailsResponse(string Number, string Type, string Status, bool IsPinSet)
{
    public static CardDetailsResponse FromCardDetails(CardDetails details)
        => new(details.CardNumber, details.CardType.ToString(), details.CardStatus.ToString(), details.IsPinSet);
}


