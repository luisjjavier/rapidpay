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
{
    [Route("api/cards")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITransactionService _transactionService;
        private static readonly SemaphoreSlim Semaphore = new(1);
        public CardsController(ICardService cardService, 
            IMapper mapper, UserManager<AppUser> userManager, ITransactionService transactionService)
        {
            _cardService = cardService;
            _mapper = mapper;
            _userManager = userManager;
            _transactionService = transactionService;
        }

        [HttpPost("create-card")]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
        {
            var newCard = _mapper.Map<Card>(cardDto);
            var user = await GetAppUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            await _cardService.CreateCardAsync(newCard, user);

            return Ok(new { message = "Card created successfully", cardNumer = newCard.CardNumber,
                cardId = newCard.Id });
        }

        private async Task<AppUser?> GetAppUserAsync()
        {
            string? email = User.GetEmail();
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentDto paymentDto)
        {
            try
            {
                await Semaphore.WaitAsync();
                var user = await GetAppUserAsync();
                if (user == null)
                {
                    return Unauthorized();
                }
                var transaction = _mapper.Map<Transaction>(paymentDto);
                var transactionResult =  await _transactionService.MakePaymentAsync(transaction, user);

                if (transactionResult.TransactionStatus == PaymentTransactionStatus.Aborted)
                {
                    return Conflict(new { message = "Insufficiency balance for make the payment" });
                }

                return Ok(new { message = "Payment processed successfully." });
            }
            finally
            {
                 Semaphore.Release();
            }
        }

        [HttpGet("{id}/balance")]
        public IActionResult GetBalance([FromRoute] Guid id)
        {
            return Ok();
        }
    }
}
