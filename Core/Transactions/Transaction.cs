
using Core.Cards;

namespace Core.Transactions
{
    /// <summary>
    /// Represents a payment transaction, inheriting from the base entity.
    /// </summary>
    public class Transaction : BaseEntity
    {
        /// <summary>
        /// Gets or sets the amount associated with the transaction.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated card.
        /// </summary>
        public Guid CardId { get; set; }

        /// <summary>
        /// Gets or sets the card associated with the transaction.
        /// </summary>
        public Card Card { get; set; } = null!;

        /// <summary>
        /// Gets or sets the status of the payment transaction.
        /// </summary>
        public PaymentTransactionStatus TransactionStatus { get; set; }
    }

}
