using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialPeople.Services;
namespace SocialPeople.UnitTests.Tests
{
    [TestClass]
    public class TwitterServiceTests
    {
        [TestMethod]
        public void Should_Result_Not_Null_When_Searching_Ali()
        {
            // Act
            TwitterService twitterService = new TwitterService();
            // Arrange
            var r = twitterService.SearchPeope("Ali");
            // Assert
            Assert.IsNotNull(r);
        }
    }
}