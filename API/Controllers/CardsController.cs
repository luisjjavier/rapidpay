using API.Models;
using Core.Cards;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpPost("create-card")]
        public IActionResult CreateCard([FromBody] CardDto cardDto)
        {
            return Ok();
        }

        [HttpPut("{id}/pay")]
        public IActionResult Pay([FromBody] CardDto cardDto)
        {
            return Ok();
        }

        [HttpGet("{id}/balance")]
        public IActionResult GetBalance([FromRoute] Guid id)
        {
            return Ok();
        }
    }
}
