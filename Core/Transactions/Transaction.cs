
using Core.Cards;

namespace Core.Transactions
{
    public class Transaction : BaseEntity
    {
        public int Amount { get; set; }

        public Guid CardId { get; set; }

        public Card Card { get; set; } = null!;

        public PaymentTransactionStatus TransactionStatus { get; set; }
    }
}
