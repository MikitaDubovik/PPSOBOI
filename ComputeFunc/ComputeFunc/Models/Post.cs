using System;
using System.Collections.Generic;

namespace ComputeFunc.Models
{
    public class Post
    {
        public string id { get; set; }

        public int? PostId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? NumberOfLikes { get; set; }

        public DateTime UploadDate { get; set; }

        public User User { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<UserLikesEntity> UserLikesEntity { get; set; }

        public byte[] Image { get; set; }

        public int? AgeId { get; set; }

        public int? CountryId { get; set; }

        public int? LanguageId { get; set; }

        public int? SexId { get; set; }

        public bool IsAd { get; set; }

        public string Price { get; set; }
    }
}
