using API.Helpers;
using API.Models;
using AutoMapper;
using Core.AppUsers;
using Core.Cards;
using Core.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{/// <summary>
 /// API controller for managing payment cards.
 /// </summary>
    [Route("api/cards")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsController"/> class.
        /// </summary>
        /// <param name="cardService">Service for managing payment cards.</param>
        /// <param name="mapper">Mapper for mapping DTOs to domain models.</param>
        /// <param name="userManager">User manager for managing application users.</param>
        /// <param name="transactionService">Service for managing payment transactions.</param>
        public CardsController(
            ICardService cardService,
            IMapper mapper,
            UserManager<AppUser> userManager,
            ITransactionService transactionService)
        {
            _cardService = cardService;
            _mapper = mapper;
            _userManager = userManager;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Creates a new payment card.
        /// </summary>
        /// <param name="cardDto">DTO containing card information.</param>
        /// <returns>
        /// 200 OK with a success message and card details if successful.
        /// 400 Bad Request with an error message if unsuccessful.
        /// </returns>
        [HttpPost("create-card")]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
        {
            var newCard = _mapper.Map<Card>(cardDto);
            var user = await GetAppUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _cardService.CreateCardAsync(newCard, user);
            if (result.Success)
            {
                return Ok(new
                {
                    message = "Card created successfully",
                    cardNumber = newCard.CardNumber,
                    cardId = newCard.Id
                });
            }
            return BadRequest(new
            {
                message = string.Join(',', result.Errors)
            });
        }

        /// <summary>
        /// Processes a payment using the specified card.
        /// </summary>
        /// <param name="paymentDto">DTO containing payment information.</param>
        /// <param name="id">Card identification key</param>
        /// <returns>
        /// 200 OK with a success message if the payment is processed successfully.
        /// 400 Bad Request with an error message if unsuccessful.
        /// 401 Unauthorized if the user is not authenticated.
        /// </returns>
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> Pay([FromRoute]Guid id, [FromBody] PaymentDto paymentDto)
        {
            paymentDto.CardId = id;
            var user = await GetAppUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            var transaction = _mapper.Map<Transaction>(paymentDto);
            var transactionResult = await _transactionService.MakePaymentAsync(transaction, user);

            if (!transactionResult.Success)
            {
                return BadRequest(new { message = string.Join(",", transactionResult.Errors) });
            }

            return Ok(new { message = "Payment processed successfully." });
        }

        /// <summary>
        /// Retrieves the balance of a payment card.
        /// </summary>
        /// <param name="id">The ID of the payment card.</param>
        /// <returns>
        /// 200 OK with the card balance if the card exists.
        /// 404 Not Found if the card does not exist.
        /// </returns>
        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetBalance([FromRoute] Guid id)
        {
            var card = await _cardService.GetCardAsync(id);
            if (card is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CardDto>(card));
        }

        private async Task<AppUser?> GetAppUserAsync()
        {
            string? email = User.GetEmail();
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

    }
}
