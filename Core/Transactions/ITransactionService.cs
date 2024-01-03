using Core.AppUsers;
using Core.Results;

namespace Core.Transactions
{
    public interface ITransactionService
    {
        Task<Result<Transaction>> MakePaymentAsync(Transaction transaction, AppUser appUser);
    }
}
