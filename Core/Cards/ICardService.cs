using Core.AppUsers;

namespace Core.Cards
{
    public  interface ICardService
    {
        Task<Card> GetCardAsync(Guid id);
        Task<Card> CreateCardAsync(Card card, AppUser appUser);
        Task UpdateAsync(Card card, AppUser appUser);
    }
}
