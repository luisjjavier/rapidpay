using API.Models;
using AutoMapper;
using Core.Cards;

namespace API.Profiles
{
    /// <summary>
    /// AutoMapper profile for mapping between <see cref="CardDto"/> and <see cref="Card"/>.
    /// </summary>
    public class CardProfiles : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardProfiles"/> class.
        /// </summary>
        public CardProfiles()
        {
            // Map CardDto to Card and vice versa
            CreateMap<CardDto, Card>().ReverseMap();
        }
    }

}
