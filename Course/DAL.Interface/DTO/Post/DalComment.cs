using System;

namespace DAL.Interface.DTO
{
    public class DalComment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public DateTime Posted { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}
