using Core.AppUsers;

namespace Core.Transactions
{
    public interface ITransactionService
    {
        Task<Transaction> MakePaymentAsync(Transaction transaction, AppUser appUser);
    }
}
