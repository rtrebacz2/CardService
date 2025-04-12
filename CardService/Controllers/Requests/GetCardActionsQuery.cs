using System.ComponentModel.DataAnnotations;

namespace CardService.Controllers.Requests;

public class GetCardActionsQuery
{
    [Required]
    [MaxLength(5)]
    public string UserId { get; set; }

    [Required]
    [MaxLength(7)]
    public string CardNumber { get; set; }
}