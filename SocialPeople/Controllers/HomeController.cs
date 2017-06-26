using SocialPeople.Models;
using SocialPeople.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
namespace SocialPeople.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        FacebookService facebookService;
        TwitterService twitterService;
        SocialNetworkDbContext _socialNetworkDbContext;
        #endregion
        #region Ctor
        public HomeController()
        {
            facebookService = new FacebookService();
            twitterService = new TwitterService();
            _socialNetworkDbContext = new SocialNetworkDbContext();
        }
        #endregion
        #region Actions and methods
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(string search)
        {
            List<PeopleModel> peopleModels = new List<PeopleModel>();
            FaceService faceService = new FaceService();

            // Search for the  full name on Facebook
            var peopleFacebook = await facebookService.SearchPeopeAsync(search);
            peopleModels.AddRange(peopleFacebook);

            // Search for the  full name on Twitter
            var peopleTwitter = twitterService.SearchPeope(search);
            peopleModels.AddRange(peopleTwitter);

            // Compare people over face recognition API
            await faceService.MatchPhotos(peopleModels);

            return View(GetPeoples(search));
        }
        /// <summary>
        /// Search that person in the database
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private List<PeopleModel> GetPeoples(string search)
        {
            return (from p in _socialNetworkDbContext.Peoples
                    where p.FullName.Contains(search)
                    select new PeopleModel
                    {
                        FullName = p.FullName,
                        PictureUrl = p.PictureUrl,
                        SocialNetworkModels = (from sn in _socialNetworkDbContext.SocialNetworkS
                                               where sn.PeopleId == p.PeopleId
                                               select new SocialNetworkModel
                                               {
                                                   SocialNetworkType = sn.SocialNetworkType,
                                                   Url = sn.Url,
                                                   Verified = sn.Verified
                                               }).ToList()
                    }).ToList();
        } 
        #endregion
    }
}//