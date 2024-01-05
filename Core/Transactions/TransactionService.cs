using Core.AppUsers;
using Core.Cards;
using Core.PaymentFees;
using Core.Results;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace Core.Transactions
{
    /// <summary>
    /// Manages payment transactions, ensuring integrity in the payment process.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly ICardService _cardService;
        private readonly IPaymentFeeService _feeService;
        private static readonly SemaphoreSlim Semaphore = new(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="transactionRepository">Repository for managing transaction data.</param>
        /// <param name="logger">Logger for logging relevant information and errors.</param>
        /// <param name="cardService">Service for card-related operations.</param>
        /// <param name="feeService">Service for calculating transaction fees.</param>
        public TransactionService(
            IRepository<Transaction> transactionRepository,
            ILogger<TransactionService> logger,
            ICardService cardService,
            IPaymentFeeService feeService)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
            _cardService = cardService;
            _feeService = feeService;
        }

        /// <summary>
        /// Initiates a payment transaction, validating the card's existence and invoking the payment processing method.
        /// </summary>
        /// <param name="transaction">The transaction to be processed.</param>
        /// <param name="appUser">The user initiating the transaction.</param>
        /// <returns>A result indicating the success or failure of the transaction.</returns>
        public async Task<Result<Transaction>> MakePaymentAsync(Transaction transaction, AppUser appUser)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    await Semaphore.WaitAsync();

                    var card = await _cardService.GetCardAsync(transaction.CardId);

                    if (card is null)
                        return Result<Transaction>.FailedResult(transaction, "Card does not exist");

                    var paymentResult = await ValidateAndProcessPayment(card, transaction, appUser);
                    scope.Complete();
                    return paymentResult;
                }
                catch (Exception e)
                {
                    string message = "Failed to make the payment";
                    _logger.LogError(e, message);
                    scope.Dispose();
                    return Result<Transaction>.FailedResult(transaction, message);
                }
                finally
                {
                    Semaphore.Release();
                }
            }
        }

        /// <summary>
        /// Performs validation and processing of a payment transaction.
        /// </summary>
        /// <param name="card">The payment card associated with the transaction.</param>
        /// <param name="transaction">The transaction to be processed.</param>
        /// <param name="appUser">The user initiating the transaction.</param>
        /// <returns>A result indicating the success or failure of the transaction.</returns>
        private async Task<Result<Transaction>> ValidateAndProcessPayment(Card card, Transaction transaction, AppUser appUser)
        {
            var fee = _feeService.GetPaymentFee();
            var newBalance = card.Balance - transaction.Amount - fee;

            if (newBalance < 0)
            {
                transaction.TransactionStatus = PaymentTransactionStatus.Aborted;
                await _transactionRepository.CreateAsync(transaction, appUser);
                return Result<Transaction>.FailedResult(transaction, "Insufficient balance to complete the payment");
            }

            card.Balance = newBalance;
            transaction.TransactionStatus = PaymentTransactionStatus.Success;

            await _cardService.UpdateAsync(card, appUser);
            await _transactionRepository.CreateAsync(transaction, appUser);

            return Result<Transaction>.SuccessResult(transaction);
        }
    }

}
