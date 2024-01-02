using API.Models;
using AutoMapper;
using Core.Transactions;

namespace API.Profiles
{
    public class TransactionProfile: Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, PaymentDto>().ReverseMap();
        }
    }
}
