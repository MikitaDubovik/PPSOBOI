using System.Data.Entity;
using ORM.Entity;
using ORM.Migrations;

namespace ORM.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("PostService")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLikesEntity> UserLikes { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Sex> Sex { get; set; }

        public DbSet<Age> Ages { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<DisabledAd> DisabledAds { get; set; }
    }
}
