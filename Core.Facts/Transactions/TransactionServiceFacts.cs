namespace Core.Facts.Transactions
{
    using Core.AppUsers;
    using Core.Cards;
    using Core.PaymentFees;
    using Core.Transactions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class TransactionServiceFacts
    {
        [Test]
        public async Task MakePaymentAsync_WhenCardDoesNotExist_ReturnsFailedResult()
        {
            // Arrange
            var cardServiceMock = new Mock<ICardService>();
            cardServiceMock.Setup(service => service.GetCardAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Card)null);

            var transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            var loggerMock = new Mock<ILogger<TransactionService>>();
            var feeServiceMock = new Mock<IPaymentFeeService>();

            var transactionService = new TransactionService(
                transactionRepositoryMock.Object,
                loggerMock.Object,
                cardServiceMock.Object,
                feeServiceMock.Object);

            var transaction = new Transaction { CardId = Guid.NewGuid() };
            var appUser = new AppUser();

            // Act
            var result = await transactionService.MakePaymentAsync(transaction, appUser);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Card does not exist", result.Errors?.GetEnumerator().Current);
        }

        [Test]
        public async Task MakePaymentAsync_WhenTransactionProcessingFails_ReturnsFailedResult()
        {
            // Arrange
            var cardServiceMock = new Mock<ICardService>();
            cardServiceMock.Setup(service => service.GetCardAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Card { Balance = 100 });

            var transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            transactionRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Transaction>(), It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Simulated exception during transaction processing"));

            var loggerMock = new Mock<ILogger<TransactionService>>();
            var feeServiceMock = new Mock<IPaymentFeeService>();

            var transactionService = new TransactionService(
                transactionRepositoryMock.Object,
                loggerMock.Object,
                cardServiceMock.Object,
                feeServiceMock.Object);

            var transaction = new Transaction { CardId = Guid.NewGuid(), Amount = 150 };
            var appUser = new AppUser();

            // Act
            var result = await transactionService.MakePaymentAsync(transaction, appUser);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Failed to make the payment", result.Errors?.GetEnumerator().Current);
        }

        [Test]
        public async Task MakePaymentAsync_WhenSuccessful_ReturnsSuccessResult()
        {
            // Arrange
            var cardServiceMock = new Mock<ICardService>();
            cardServiceMock.Setup(service => service.GetCardAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Card { Balance = 100 });

            var transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            var loggerMock = new Mock<ILogger<TransactionService>>();
            var feeServiceMock = new Mock<IPaymentFeeService>();

            var transactionService = new TransactionService(
                transactionRepositoryMock.Object,
                loggerMock.Object,
                cardServiceMock.Object,
                feeServiceMock.Object);

            var transaction = new Transaction { CardId = Guid.NewGuid(), Amount = 50 };
            var appUser = new AppUser();

            // Act
            var result = await transactionService.MakePaymentAsync(transaction, appUser);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(PaymentTransactionStatus.Success, transaction.TransactionStatus);
        }
    }

}
