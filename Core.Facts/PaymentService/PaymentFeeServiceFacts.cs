namespace Core.Facts.PaymentService
{
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PaymentFeeServiceFacts
    {
        [Test]
        public void GetPaymentFee_ShouldReturn_LastFeeAmount()
        {
            // Arrange
            var configurationMock = BuildConfigurationBinder();

            var paymentService = new PaymentFees.PaymentService(configurationMock);

            // Act
            var result = paymentService.GetPaymentFee();

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CalculatePaymentFee_ShouldReturn_NewFee()
        {
            // Arrange
            var configurationMock = BuildConfigurationBinder();

            var paymentService = new PaymentFees.PaymentService(configurationMock);

            // Act
            var result = paymentService.CalculatePaymentFee();

            // Assert
            Assert.GreaterOrEqual(result, 0);
        }

        [Test]
        public void CalculatePaymentFee_ShouldUpdate_LastFeeAmount()
        {
            // Arrange
            var configurationMock = BuildConfigurationBinder();

            var paymentService = new PaymentFees.PaymentService(configurationMock);

            // Act
            var initialLastFeeAmount = paymentService.GetPaymentFee();
            var result = paymentService.CalculatePaymentFee();
            var updatedLastFeeAmount = paymentService.GetPaymentFee();

            // Assert
            Assert.AreNotEqual(initialLastFeeAmount, updatedLastFeeAmount);
        }

        private IConfigurationRoot BuildConfigurationBinder()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
            configuration["InitialFeeAmount"] = "1";
            return configuration;
        }
    }
}
