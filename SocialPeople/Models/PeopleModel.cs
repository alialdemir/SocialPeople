using System.Collections.Generic;

namespace SocialPeople.Models
{
    public class PeopleModel
    {
        public string PictureUrl { get; set; }
        public string FullName { get; set; }
        public bool Verified { get; set; }
        public List<SocialNetworkModel> SocialNetworkModels { get; set; } = new List<SocialNetworkModel>();
    }
}