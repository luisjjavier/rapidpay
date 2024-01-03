using Core.AppUsers;
using Core.Helpers;
using Core.Results;
using Microsoft.Extensions.Logging;

namespace Core.Cards
{
    /// <summary>
    ///  Service responsible for managing Card entities.
    /// </summary>
    public class CardService : ICardService
    {
        private readonly IRepository<Card> _cardRepository;
        private readonly ILogger<CardService> _logger;


        /// <summary>
        ///  Initializes a new instance of the CardService class.
        /// </summary>
        /// <param name="cardRepository"></param>
        /// <param name="logger"></param>
        public CardService(IRepository<Card> cardRepository, ILogger<CardService> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a card by its ID asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Card?> GetCardAsync(Guid id)
        {
            var card = await _cardRepository.GetByIdAsync(id);
            return card;
        }

        /// <summary>
        /// Creates a new card asynchronously.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task<Result<Card>> CreateCardAsync(Card card, AppUser appUser)
        {
            try
            {
                return await TryToCreateCardAsync(card, appUser);
            }
            catch (Exception e)
            {
                string message = "Failed to create a card";
                _logger.LogError(e, message);
                return Result<Card>.FailedResult(card, message);
            }
        }

        /// <summary>
        /// Tries to create a new card asynchronously, handling exceptions.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        private async Task<Result<Card>> TryToCreateCardAsync(Card card, AppUser appUser)
        {
            _logger.LogTrace("Creating a new card");

            if (string.IsNullOrEmpty(card.CardNumber))
            {
                card.CardNumber = GenerateCardNumber();
            }

            var entity = await _cardRepository.CreateAsync(card, appUser);

            return Result<Card>.SuccessResult(entity);
        }

        /// <summary>
        /// Updates a card asynchronously.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Card card, AppUser appUser) => await _cardRepository.UpdateAsync(card, appUser);

        /// <summary>
        /// Generates a random card number.
        /// </summary>
        /// <returns></returns>
        private string GenerateCardNumber()
        {
            Random random = new Random();
            long min = 100_000_000_000_000;
            long max = 999_999_999_999_999;

            return random.NextLong(min, max).ToString();
        }
    }
}
