using System.ComponentModel.DataAnnotations;
namespace SocialPeople.Tables
{
    /// <summary>
    /// People's social network account information
    /// </summary>
    public class SocialNetwork
    {
        /// <summary>
        /// Id
        /// </summary>
        public int SocialNetworkId { get; set; }
        /// <summary>
        /// Which social network it belongs
        /// </summary>
        public SocialNetworkTypes SocialNetworkType { get; set; }
        /// <summary>
        /// People entity relationship
        /// </summary>
        public int PeopleId { get; set; }
        public People People { get; set; }
        /// <summary>
        /// Social account url
        /// </summary>
        [MaxLength(255)]
        public string Url { get; set; }
        /// <summary>
        /// Social network account approval status
        /// </summary>
        public bool Verified { get;  set; }
    }
}