namespace API.Models
{
    public class CardDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
