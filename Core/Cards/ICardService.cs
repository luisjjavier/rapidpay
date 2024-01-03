using Core.AppUsers;
using Core.Results;

namespace Core.Cards
{
    public  interface ICardService
    {
        Task<Card?> GetCardAsync(Guid id);
        Task<Result<Card>> CreateCardAsync(Card card, AppUser appUser);
        Task UpdateAsync(Card card, AppUser appUser);
    }
}
