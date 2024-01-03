namespace Core.Cards
{
    /// <summary>
    /// Represents a payment card in the RapidPay system.
    /// </summary>
    public class Card : BaseEntity
    {
        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        public string CardNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the balance of the card.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the status of the card.
        /// </summary>
        public CardStatus Status { get; set; } = CardStatus.Active;
    }

}
