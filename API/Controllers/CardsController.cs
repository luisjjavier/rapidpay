using API.Models;
using AutoMapper;
using Core.Cards;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardsController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }

        [HttpPost("create-card")]
        public IActionResult CreateCard([FromBody] CardDto cardDto)
        {
            var newCard = _mapper.Map<Card>(cardDto);

            _cardService.CreateCardAsync(newCard);

            return Ok(new { message = "Card created successfully", cardNumer = newCard.CardNumber });
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
