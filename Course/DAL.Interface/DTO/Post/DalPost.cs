using System;
using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalPost
    {
        public int PostId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfLikes { get; set; }

        public DateTime UploadDate { get; set; }

        public DalUser User { get; set; }

        public IEnumerable<DalTag> Tags { get; set; }

        public IEnumerable<DalUserLikesEntity> UserLikesEntity { get; set; }

        public byte[] Image { get; set; }

        public int? AgeId { get; set; }

        public int? CountryId { get; set; }

        public int? LanguageId { get; set; }

        public int? SexId { get; set; }

        public bool IsAd { get; set; }

        public string Price { get; set; }
    }
}
