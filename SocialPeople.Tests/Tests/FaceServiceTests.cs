using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialPeople.Models;
using SocialPeople.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialPeople.UnitTests.Tests
{
    [TestClass]
    public class FaceServiceTests
    {
        [TestMethod]
        public async Task MatchPhotos()
        {
            List<PeopleModel> peoples = new List<PeopleModel>();
            FacebookService facebookService = new FacebookService();
            TwitterService twitterService = new TwitterService();
            FaceService faceService = new FaceService();
            string searchFullName = "Ali";

            var peopleFacebook = await facebookService.SearchPeopeAsync(searchFullName);
            var peopleTwitter = twitterService.SearchPeope(searchFullName);

            peoples.AddRange(peopleFacebook);
            peoples.AddRange(peopleTwitter);

            await    faceService.MatchPhotos(peoples);
        }
    }
}