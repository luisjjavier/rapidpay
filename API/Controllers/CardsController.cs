using API.Helpers;
using API.Models;
using AutoMapper;
using Core.AppUsers;
using Core.Cards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CardsController(ICardService cardService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _cardService = cardService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("create-card")]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
        {
            var newCard = _mapper.Map<Card>(cardDto);
            string email = User.GetEmail();
            var user = await _userManager.FindByEmailAsync(email);
            await _cardService.CreateCardAsync(newCard, user!);

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
