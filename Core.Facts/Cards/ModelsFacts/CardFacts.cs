using Core.Cards;

namespace Core.Facts.Cards.ModelsFacts
{
    [TestFixture]
    public class CardFacts
    {
        [Test]
        public void Card_Initialization()
        {
            // Arrange
            string expectedCardNumber = "123456789012345";
            decimal expectedBalance = 100.0m;

            // Act
            var card = new Card
            {
                CardNumber = expectedCardNumber,
                Balance = expectedBalance
            };

            // Assert
            Assert.AreEqual(expectedCardNumber, card.CardNumber);
            Assert.AreEqual(expectedBalance, card.Balance);
        }
    }
}
