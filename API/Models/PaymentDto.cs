namespace API.Models
{
    public class PaymentDto
    {
        public Guid CardId { get; set; }
        public int Amount { get; set; }
    }
}
