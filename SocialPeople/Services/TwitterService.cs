using SocialPeople.Models;
using System;
using System.Collections.Generic;
using Tweetinvi;
using System.Linq;
namespace SocialPeople.Services
{
    public class TwitterService
    {
        #region Field
        /// <summary>
        /// Set your Twitter consumer key
        /// </summary>
        private string ConsumerKey { get; } = "Set your Twitter consumer key";
        /// <summary>
        /// Set your Twitter consumer secret
        /// </summary>
        private string ConsumerSecret { get; } = "Set your Twitter consumer secret";
        /// <summary>
        /// Set your Twitter user access token
        /// </summary>
        private string UserAccessToken { get; } = "Set your Twitter user access token";
        /// <summary>
        /// Set your Twitter user access secret
        /// </summary>
        private string UserAccessSecret { get; } = "Set your Twitter user access secret";
        /// <summary>
        /// Undesirable pictures. Default profile picture etc.
        /// </summary>
        private string[] UnwantedImages = new string[] { "http://abs.twimg.com/sticky/default_profile_images/default_profile.png" };
        #endregion
        #region Ctor
        public TwitterService()
        {
            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials(ConsumerKey, ConsumerSecret, UserAccessToken, UserAccessSecret);
        }
        #endregion
        #region Method
        /// <summary>
        /// People search on Facebook
        /// </summary>
        /// <param name="fullName">Searched human name surname</param>
        /// <returns></returns>
        public List<PeopleModel> SearchPeope(string fullName)
        {
            if (String.IsNullOrEmpty(fullName))
                throw new ArgumentException(fullName);

            List<PeopleModel> result = new List<PeopleModel>();
            int pageIndex = 0;
            while (true)
            {
                var newList = Search
                            .SearchUsers(fullName.ToLower(), page: pageIndex)
                            .Where(p => !UnwantedImages.Contains(p.ProfileImageUrlFullSize))
                            .Select(p => new PeopleModel
                            {
                                FullName = p.Name,
                                PictureUrl = p.ProfileImageUrlFullSize,
                                Verified = p.Verified,
                                SocialNetworkModels = new List<SocialNetworkModel>
                                {
                                    new SocialNetworkModel { Url = "http://twitter.com/" + p.ScreenName, SocialNetworkType= SocialNetworkTypes.Twitter }
                                }
                            }).ToList();
                result.AddRange(newList);
                if (pageIndex > 25 || newList.Count < 20)// The end of the page or if less than the number of records exit the loop
                    break;
                pageIndex++;
            }
            return result;
        }
        #endregion
    }
}