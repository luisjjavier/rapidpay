using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    public class PaymentDto
    {
        [FromRoute]
        public Guid CardId { get; set; }
        public int Amount { get; set; }
    }
}
