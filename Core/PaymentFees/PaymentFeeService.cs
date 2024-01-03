namespace Core.PaymentFees
{
    /// <summary>
    /// Service responsible for calculating payment fees using a random and dynamic approach.
    /// </summary>
    public class PaymentService: IPaymentFeeService
    {
        private readonly Random _random = new Random();
        private decimal _lastFeeAmount = 1.0m; // Initial fee amount

        /// <summary>
        /// Calculates the payment fee based on a random decimal selected every hour by the Universal Fees Exchange (UFE).
        /// The new fee price is the last fee amount multiplied by the recent random decimal.
        /// </summary>
        /// <returns>The calculated payment fee.</returns>
        public decimal CalculatePaymentFee()
        {
            double randomDecimal = _random.NextDouble() * 2;
            decimal newFee = _lastFeeAmount * (decimal)randomDecimal;
            _lastFeeAmount = newFee;
            return newFee;
        }
    }

}
