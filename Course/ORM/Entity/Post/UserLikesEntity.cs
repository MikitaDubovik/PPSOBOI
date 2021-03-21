namespace ORM.Entity
{
    public class UserLikesEntity
    {
        public int UserLikesEntityId { get; set; }

        public int UserLikesId { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}