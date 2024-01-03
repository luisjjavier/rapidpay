namespace Core.PaymentFees
{
    /// <summary>
    /// Defines the contract for a service responsible for calculating payment fees.
    /// </summary>
    public interface IPaymentFeeService
    {
        /// <summary>
        /// Calculates the payment fee for a transaction.
        /// </summary>
        /// <returns>The calculated payment fee as a decimal value.</returns>
        decimal CalculatePaymentFee();
    }

}
