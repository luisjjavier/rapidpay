using Core.AppUsers;
using Core.Results;

namespace Core.Cards
{
    /// <summary>
    /// Provides operations related to payment cards in the RapidPay system.
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Retrieves a payment card by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the card to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved card or null if not found.</returns>
        Task<Card?> GetCardAsync(Guid id);

        /// <summary>
        /// Creates a new payment card.
        /// </summary>
        /// <param name="card">The card information for creation.</param>
        /// <param name="appUser">The user initiating the card creation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the card creation operation.</returns>
        Task<Result<Card>> CreateCardAsync(Card card, AppUser appUser);

        /// <summary>
        /// Updates the information of an existing payment card.
        /// </summary>
        /// <param name="card">The card information to update.</param>
        /// <param name="appUser">The user initiating the card update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Card card, AppUser appUser);
    }

}
