using Microsoft.ProjectOxford.Face;
using SocialPeople.Models;
using SocialPeople.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialPeople.Services
{
    public class FaceService
    {
        #region Fields
        /// <summary>
        /// Set your Face api key
        /// https://docs.microsoft.com/tr-tr/azure/cognitive-services/face/quickstarts/csharp
        /// </summary>
        private string SubscriptionKey { get; } = "Set your Face api key";
        private readonly SocialNetworkDbContext _socialNetworkDbContext;
        #endregion
        #region Ctor
        public FaceService()
        {
            _socialNetworkDbContext = new SocialNetworkDbContext();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Compare people over face recognition API
        /// </summary>
        /// <param name="peopleModels">People Informations</param>
        public async Task MatchPhotos(List<PeopleModel> peopleModels)
        {
            FaceServiceClient client = new FaceServiceClient(SubscriptionKey, "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");

            List<PeopleModel> peopleFacebookList = peopleModels.Where(p => p.SocialNetworkModels.Where(x => x.SocialNetworkType.HasFlag(SocialNetworkTypes.Facebook)).Any()).ToList();
            List<PeopleModel> peopleTwitterList = peopleModels.Where(p => p.SocialNetworkModels.Where(x => x.SocialNetworkType.HasFlag(SocialNetworkTypes.Twitter)).Any()).ToList();

            foreach (var facebookPeople in peopleFacebookList)
            {
                try
                {
                    var faces1 = await client.DetectAsync(facebookPeople.PictureUrl);
                    foreach (var twitterPeople in peopleTwitterList)
                    {
                        Thread.SpinWait(500);// It's blocking when you send it fast. So we're waiting. :)
                        try
                        {
                            var faces2 = await client.DetectAsync(twitterPeople.PictureUrl);

                            if (!(faces1 == null || faces2 == null) && !(faces1.Count() == 0 || faces2.Count() == 0) && !(faces1.Count() > 1 || faces2.Count() > 1))
                            {
                                try
                                {
                                    var res = await client.VerifyAsync(faces1[0].FaceId, faces2[0].FaceId);
                                    double score = 0;
                                    if (res.IsIdentical)
                                        score = 100;
                                    else
                                    {
                                        score = Math.Round((res.Confidence / 0.5) * 100);
                                    }

                                    if (score >= 70 /*&& ((score == 100) ||(iPeople.Verified && jPeople.Verified) || (!iPeople.Verified && !jPeople.Verified))*/)
                                    {
                                        InsertPeople(facebookPeople, twitterPeople, score);
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

        }
        /// <summary>
        /// if there is a similarity Saving to database 
        /// </summary>
        /// <param name="facebookPeople">Facebook people informations</param>
        /// <param name="twitterPeople">Twitter people informations</param>
        /// <param name="score">Similarity score</param>
        private void InsertPeople(PeopleModel facebookPeople, PeopleModel twitterPeople, double score)
        {
            People people = new People
            {
                FullName = facebookPeople.FullName,
                PictureUrl = facebookPeople.PictureUrl,
                Score = (byte)score,
            };
            _socialNetworkDbContext.Peoples.Add(people);
            _socialNetworkDbContext.SaveChanges();
            // Facebook
            foreach (var facebook in facebookPeople.SocialNetworkModels)
            {
                _socialNetworkDbContext.SocialNetworkS.Add(new SocialNetwork
                {
                    PeopleId = people.PeopleId,
                    SocialNetworkType = facebook.SocialNetworkType,
                    Url = facebook.Url,
                    Verified = facebookPeople.Verified

                });
                _socialNetworkDbContext.SaveChanges();
            }
            // Twitter
            foreach (var twitter in twitterPeople.SocialNetworkModels)
            {
                _socialNetworkDbContext.SocialNetworkS.Add(new SocialNetwork
                {
                    PeopleId = people.PeopleId,
                    SocialNetworkType = twitter.SocialNetworkType,
                    Url = twitter.Url,
                    Verified = twitterPeople.Verified
                });
                _socialNetworkDbContext.SaveChanges();
            }
        }
        #endregion
    }
}