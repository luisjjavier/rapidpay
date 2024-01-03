using Core.AppUsers;
using Core.Results;

namespace Core.Transactions
{
    /// <summary>
    /// Defines the contract for a service responsible for managing payment transactions.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Initiates a payment transaction, ensuring integrity in the payment process.
        /// </summary>
        /// <param name="transaction">The transaction to be processed.</param>
        /// <param name="appUser">The user initiating the transaction.</param>
        /// <returns>A result indicating the success or failure of the transaction.</returns>
        Task<Result<Transaction>> MakePaymentAsync(Transaction transaction, AppUser appUser);
    }

}
