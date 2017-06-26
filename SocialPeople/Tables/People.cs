using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialPeople.Tables
{
    /// <summary>
    /// People entity
    /// </summary>
    public class People
    {
        /// <summary>
        /// Id
        /// </summary>
        public int PeopleId { get; set; }
        /// <summary>
        /// Name and surname
        /// </summary>
        [MaxLength(100)]
        public string FullName { get; set; }
        /// <summary>
        /// Facebook profile picture url 
        /// </summary>
        [MaxLength(300)]
        public string PictureUrl { get; set; }
        /// <summary>
        /// Similarity score
        /// </summary>
        public byte Score { get; set; }
        public virtual ICollection<SocialNetwork> SocialNetworks { get; set; } = new HashSet<SocialNetwork>();
    }
}