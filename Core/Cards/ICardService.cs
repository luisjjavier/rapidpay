namespace Core.Cards
{
    public  interface ICardService
    {
        Task<Card> GetCard(Guid id);
        Task<Card> CreateCardAsync(Card card);
    }
}
