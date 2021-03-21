namespace ORM.Entity
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Text { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}