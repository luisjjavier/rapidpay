using Core.AppUsers;
using Core.Cards;
using Microsoft.Extensions.Logging;
using Moq;

namespace Core.Facts.Cards
{
    [TestFixture]
    public class CardServiceFacts
    {
        [Test]
        public async Task GetCardAsync_ValidId_ReturnsCard()
        {
            // Arrange
            var cardRepositoryMock = new Mock<IRepository<Card>>();
            var loggerMock = new Mock<ILogger<CardService>>();
            var cardService = new CardService(cardRepositoryMock.Object, loggerMock.Object);

            var cardId = Guid.NewGuid();
            var expectedCard = new Card { Id = cardId };
            cardRepositoryMock.Setup(repo => repo.GetByIdAsync(cardId, It.IsAny<CancellationToken>())).ReturnsAsync(expectedCard);

            // Act
            var result = await cardService.GetCardAsync(cardId);

            // Assert
            Assert.AreEqual(expectedCard, result);
        }

        [Test]
        public async Task CreateCardAsync_ValidInput_ReturnsSuccessResult()
        {
            // Arrange
            var cardRepositoryMock = new Mock<IRepository<Card>>();
            var loggerMock = new Mock<ILogger<CardService>>();
            var cardService = new CardService(cardRepositoryMock.Object, loggerMock.Object);

            var card = new Card();
            var appUser = new AppUser();

            // Mock repository behavior
            cardRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Card>(), It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new Card());

            // Act
            var result = await cardService.CreateCardAsync(card, appUser);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Item);
        }

        [Test]
        public async Task CreateCardAsync_ExceptionOccurs_ReturnsFailedResult()
        {
            // Arrange
            var cardRepositoryMock = new Mock<IRepository<Card>>();
            var loggerMock = new Mock<ILogger<CardService>>();
            var cardService = new CardService(cardRepositoryMock.Object, loggerMock.Object);

            var card = new Card();
            var appUser = new AppUser();

            // Mock repository behavior to throw an exception
            cardRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Card>(), It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new Exception("Simulated exception"));

            // Act
            var result = await cardService.CreateCardAsync(card, appUser);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Errors);
        }

        [Test]
        public async Task UpdateAsync_ValidInput_CallsRepositoryUpdateAsync()
        {
            // Arrange
            var cardRepositoryMock = new Mock<IRepository<Card>>();
            var loggerMock = new Mock<ILogger<CardService>>();
            var cardService = new CardService(cardRepositoryMock.Object, loggerMock.Object);

            var card = new Card();
            var appUser = new AppUser();

            // Act
            await cardService.UpdateAsync(card, appUser);

            // Assert
            cardRepositoryMock.Verify(repo => repo.UpdateAsync(card, appUser, It.IsAny<CancellationToken>()), Times.Once);
        }

        // Add more test cases as needed for specific scenarios.
    }

}
