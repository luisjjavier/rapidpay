using Core.AppUsers;
using Core.Cards;
using Microsoft.Extensions.Logging;

namespace Core.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly ICardService _cardService;

        public TransactionService(IRepository<Transaction> transactionRepository, ILogger<TransactionService> logger, ICardService cardService)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
            _cardService = cardService;
        }
        public async Task<Transaction> MakePaymentAsync(Transaction transaction, AppUser appUser)
        {
            var card = await _cardService.GetCard(transaction.CardId);

            if (card.Balance - transaction.Amount < 0)
            {
                transaction.TransactionStatus = PaymentTransactionStatus.Aborted;
            }
            else
            {
                card.Balance = -transaction.Amount;
                transaction.TransactionStatus = PaymentTransactionStatus.Success;
                await _cardService.UpdateAsync(card, appUser);
            }
            await _transactionRepository.CreateAsync(transaction, appUser);

            return transaction;
        }
    }
}
