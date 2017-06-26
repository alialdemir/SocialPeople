using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialPeople.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using SocialPeople.Models;

namespace SocialPeople.UnitTests.Tests
{
    [TestClass]
    public class FacebookServiceTests
    {
        [TestMethod]
        public async Task Should_Result_Not_Null_When_Searching_Ali()
        {
            // Arrange
            FacebookService facebookService = new FacebookService();
            // Act
            List<PeopleModel> models = await facebookService.SearchPeopeAsync("Alİ");
            // Assert
            Assert.IsNotNull(models);
        }
    }
}
