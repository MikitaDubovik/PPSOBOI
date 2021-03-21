using System;
using System.Collections.Generic;

namespace MvcPL.Models
{
    public class PostDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfLikes { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserId { get; set; }
        public PostOwnerViewModel Owner { get; set; }
        public int CurrentUserId { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<int> UserLikesIds { get; set; }
        public List<string> UserLikesLogins { get; set; }
        public byte[] Image { get; set; }
    }
}