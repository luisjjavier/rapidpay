namespace Core.Facts.PaymentService
{
    [TestFixture]
    public class PaymentFeeServiceFacts
    {
        [Test]
        public void CalculatePaymentFee_ShouldReturnNonZeroFee()
        {
            // Arrange
            var paymentService = new PaymentFees.PaymentService();

            // Act
            decimal paymentFee = paymentService.CalculatePaymentFee();

            // Assert
            Assert.That(paymentFee, Is.GreaterThan(0));
        }

        [Test]
        public void CalculatePaymentFee_ShouldChangeFeeBetweenCalls()
        {
            // Arrange
            var paymentService = new PaymentFees.PaymentService();

            // Act
            decimal initialFee = paymentService.CalculatePaymentFee();
            decimal newFee = paymentService.CalculatePaymentFee();

            // Assert
            Assert.That(newFee, Is.Not.EqualTo(initialFee));
        }
    }
}
