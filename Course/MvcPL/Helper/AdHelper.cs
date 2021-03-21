using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;

namespace MvcPL.Helper
{
    public static class AdHelper
    {
        public static List<BllPost> AdPosts { get; set; } = new List<BllPost>();

        public static BllPost GetAd(List<BllPost> disabledPosts, int? ageId, int? sexId, int? countryId, int? languageId)
        {
            var tempList = new List<BllPost>();

            foreach (var adPost in AdPosts)
            {
                var flag = false;
                foreach (var disabledPost in disabledPosts)
                {
                    if (adPost.PostId == disabledPost.PostId)
                    {
                        flag = true;
                    }
                }

                if (!flag)
                {
                    tempList.Add(adPost);
                }
            }

            var temp = tempList.Any() ? tempList : AdPosts;
            
            if (ageId.HasValue)
            {
                temp = temp.Where(t => t.AgeId == ageId).ToList();
            }

            if (!temp.Any())
            {
                temp = tempList;
            }

            var superTemp = temp;

            if (sexId.HasValue)
            {
                temp = temp.Where(t => t.AgeId == sexId).ToList();
            }

            if (!temp.Any())
            {
                temp = superTemp;
            }
            else
            {
                superTemp = temp;
            }

            if (countryId.HasValue)
            {
                temp = temp.Where(t => t.AgeId == countryId).ToList();
            }

            if (!temp.Any())
            {
                temp = superTemp;
            }
            else
            {
                superTemp = temp;
            }

            if (languageId.HasValue)
            {
                temp = temp.Where(t => t.AgeId == languageId).ToList();
            }

            if (!temp.Any())
            {
                temp = superTemp;
            }

            if (!(AdPosts.Count > 0))
                return null;

            var random = new Random();
            var index = random.Next(0, temp.Count - 1);

            return temp.ElementAt(index);
        }

        public static BllPost GetRandomAd()
        {
            if (!(AdPosts.Count > 0))
                return null;

            var random = new Random();
            var index = random.Next(0, AdPosts.Count);

            return AdPosts.ElementAt(index);
        }

        public static void Initialize(IEnumerable<BllPost> newPost)
        {
            if (newPost.Any())
            {
                AdPosts = new List<BllPost>(newPost);
            }
        }
    }
}