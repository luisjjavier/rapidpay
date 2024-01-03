using API.Models;
using AutoMapper;
using Core.Transactions;

namespace API.Profiles
{
    /// <summary>
    /// AutoMapper profile for mapping between <see cref="Transaction"/> and <see cref="PaymentDto"/>.
    /// </summary>
    public class TransactionProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionProfile"/> class.
        /// </summary>
        public TransactionProfile()
        {
            // Map Transaction to PaymentDto and vice versa
            CreateMap<Transaction, PaymentDto>().ReverseMap();
        }
    }
}
