using API.Models;
using AutoMapper;
using Core.Cards;

namespace API.Profiles
{
    public class CardProfiles : Profile
    {
        public CardProfiles()
        {
            CreateMap<CardDto, Card>().ReverseMap();

        }
    }
}
