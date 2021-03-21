using System;

namespace BLL.Interface.Entities
{
    public class BllComment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public DateTime Posted { get; set; }
        public string Text { get; set; }
        public BllUser User { get; set; }
    }
}
