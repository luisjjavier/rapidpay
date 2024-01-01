using Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Core.Cards
{
    public class CardService : ICardService
    {
        private readonly IRepository<Card> _cardRepository;
        private readonly ILogger<CardService> _logger;

        public CardService(IRepository<Card> cardRepository, ILogger<CardService> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
        }
        public async Task<Card> GetCard(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Card> CreateCardAsync(Card card)
        {
            _logger.LogTrace("Creating a new card");
            if (string.IsNullOrEmpty(card.CardNumber))
            {
                card.CardNumber = GenerateCardNumber();
            }

            if (card.Balance <= 0)
            {

            }

            var entity = await _cardRepository.CreateAsync(card);

            return entity;
        }

        private string GenerateCardNumber()
        {
            Random random = new Random();
            long min = 100_000_000_000_000;
            long max = 999_999_999_999_999;

            return random.NextLong(min, max).ToString();
        }
    }
}
