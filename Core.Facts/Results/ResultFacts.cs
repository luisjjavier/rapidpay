namespace Core.Facts.Results
{
    using Core.Results;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class ResultFacts
    {
        [Test]
        public void Constructor_DefaultConstructor_SuccessIsFalse()
        {
            // Arrange
            var result = new Result<string>();

            // Assert
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void Constructor_WithSuccessAndItem_SuccessAndItemPropertiesAreSet()
        {
            // Arrange
            var result = new Result<string>(true, "example");

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("example", result.Item);
            Assert.IsEmpty(result.Errors);
        }

        [Test]
        public void Constructor_WithSuccessItemAndErrors_SuccessItemAndErrorsPropertiesAreSet()
        {
            // Arrange
            var errors = new List<string> { "error1", "error2" };
            var result = new Result<string>(false, "example", errors);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("example", result.Item);
            CollectionAssert.AreEqual(errors, result.Errors);
        }

        [Test]
        public void SuccessResult_StaticMethod_SuccessResultWithItem()
        {
            // Arrange
            var successResult = Result<string>.SuccessResult("example");

            // Assert
            Assert.IsTrue(successResult.Success);
            Assert.AreEqual("example", successResult.Item);
            Assert.IsEmpty(successResult.Errors);
        }

        [Test]
        public void FailedResult_StaticMethod_FailedResultWithItemAndErrors()
        {
            // Arrange
            var failedResult = Result<string>.FailedResult("example", "error");

            // Assert
            Assert.IsFalse(failedResult.Success);
            Assert.AreEqual("example", failedResult.Item);
            CollectionAssert.AreEqual(new List<string> { "error" }, failedResult.Errors);
        }
    }

}
