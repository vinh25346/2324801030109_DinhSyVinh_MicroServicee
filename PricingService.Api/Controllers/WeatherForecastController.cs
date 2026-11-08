using Microsoft.AspNetCore.Mvc;
using PricingService.Api.Commands;

namespace PricingService.Api.Controllers;

[ApiController]
[Route("api/pricing")]
public class PricingController : ControllerBase
{
    [HttpPost]
    public ActionResult<CalculatePriceResult> Calculate([FromBody] CalculatePriceCommand command)
    {
        var days = Math.Max(1, (command.PolicyTo.Date - command.PolicyFrom.Date).Days);
        var insuredPeople = command.Answers
            .Where(answer => answer.QuestionCode is "NUM_OF_ADULTS" or "NUM_OF_CHILDREN")
            .Sum(answer => Convert.ToDecimal(answer.GetAnswer()));
        var multiplier = Math.Max(1, insuredPeople);

        var coverPrices = command.SelectedCovers.ToDictionary(
            cover => cover,
            cover => Math.Round(days * multiplier * (cover == "C2" ? 12m : 6m), 2));

        return Ok(new CalculatePriceResult
        {
            CoverPrices = coverPrices,
            TotalPrice = coverPrices.Values.Sum()
        });
    }
}
