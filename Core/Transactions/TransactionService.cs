using Core.AppUsers;
using Core.Cards;
using Core.PaymentFees;
using Microsoft.Extensions.Logging;

namespace Core.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly ICardService _cardService;
        private readonly IPaymentFeeService _feeService;

        public TransactionService(IRepository<Transaction> transactionRepository, ILogger<TransactionService> logger, ICardService cardService,
            IPaymentFeeService feeService)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
            _cardService = cardService;
            _feeService = feeService;
        }
        public async Task<Transaction> MakePaymentAsync(Transaction transaction, AppUser appUser)
        {
            var card = await _cardService.GetCardAsync(transaction.CardId);
            var newBalance = card.Balance - transaction.Amount - _feeService.CalculatePaymentFee();
            if (newBalance < 0)
            {
                transaction.TransactionStatus = PaymentTransactionStatus.Aborted;
            }
            else
            {
                card.Balance = newBalance;
                transaction.TransactionStatus = PaymentTransactionStatus.Success;
                await _cardService.UpdateAsync(card, appUser);
            }
            await _transactionRepository.CreateAsync(transaction, appUser);

            return transaction;
        }
    }
}
