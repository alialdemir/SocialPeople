using Facebook;
using SocialPeople.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SocialPeople.Services
{
    public class FacebookService
    {
        #region Fields
        private FacebookClient _facebookClient;
        /// <summary>
        /// Set your Facebook app Id
        /// </summary>
        private string AppId { get; } = "Set your Facebook app Id";
        /// <summary>
        /// Set your Facebook app secret
        /// </summary>
        private string AppSecret { get; } = "Set your Facebook app secret";
        /// <summary>
        /// Set your Facebook access token
        /// </summary>
        private string AccessToken { get; } = "Set your Facebook access token";
        /// <summary>
        /// Undesirable pictures. Default profile picture etc.
        /// </summary>
        private string[] UnwantedImages = new string[] {
            "https://scontent.xx.fbcdn.net/v/t31.0-1/10506738_10150004552801856_220367501106153455_o.jpg?oh=8e1faeaa0f5bc35f8204d59d596467c2&oe=59D5AABB",
            "https://scontent.xx.fbcdn.net/v/t31.0-1/1402926_10150004552801901_469209496895221757_o.jpg?oh=876a5efc8635aafba1845652a21f84fa&oe=59D88B4F"
            };
        #endregion
        #region Ctor
        /// <summary>
        /// Create FacebookClient object   
        /// </summary>
        public FacebookService()
        {
            _facebookClient = new FacebookClient(AccessToken)
            {
                AppId = AppId,
                AppSecret = AppSecret
            };
        }
        #endregion
        #region Method
        /// <summary>
        /// People search on Facebook
        /// </summary>
        /// <param name="fullName">Searched human name surname</param>
        /// <returns></returns>
        public async Task<List<PeopleModel>> SearchPeopeAsync(string fullName)
        {
            if (String.IsNullOrEmpty(fullName))
                throw new ArgumentException(fullName);
            List<PeopleModel> result = new List<PeopleModel>();
            string url = $"search?q={fullName.ToLower()}&type=user&fields=name,picture.width(9999),is_verified,link&limit=1000";

            while (true)
            {
                var response = await _facebookClient.GetTaskAsync<RootObject>(url);
                if (response.paging == null || response.data.Count == 0)
                    break;

                var newList = response
                         .data
                         .Where(p => !UnwantedImages.Contains(p.picture.data.url))
                         .Select(p => new PeopleModel
                         {
                             FullName = p.name,
                             PictureUrl = p.picture.data.url,
                             Verified = p.is_verified,
                             SocialNetworkModels = new List<SocialNetworkModel>
                             {
                                 new SocialNetworkModel { SocialNetworkType = SocialNetworkTypes.Facebook, Url = p.link }
                             }
                         })
                         .ToList();
                result.AddRange(newList);
                url = response.paging.next;// Next page
            }
            return result;
        }
        #endregion
    }
}