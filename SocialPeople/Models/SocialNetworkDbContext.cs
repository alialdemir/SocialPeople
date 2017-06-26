using SocialPeople.Tables;
using System.Data.Entity;

namespace SocialPeople.Models
{
    public class SocialNetworkDbContext : DbContext
    {
        public SocialNetworkDbContext():base("SocialPeopleContext")
        {

        }
        public IDbSet<People> Peoples { get; set; }
        public IDbSet<SocialNetwork> SocialNetworkS { get; set; }
    }
}