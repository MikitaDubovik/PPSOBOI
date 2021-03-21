using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entity
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        
        public int RoleId { get; set; }

        public Role Roles { get; set; }

        [Required]
        public string Login { get; set; }

        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }

        public byte[] ProfilePhoto { get; set; }

        public int? AgeId { get; set; }

        [ForeignKey(nameof(AgeId))]
        public virtual Age Age { get; set; }

        public int? CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; }

        public int? LanguageId { get; set; }

        [ForeignKey(nameof(LanguageId))]
        public virtual Language Language { get; set; }

        public int? SexId { get; set; }

        [ForeignKey(nameof(SexId))]
        public virtual Sex Sex { get; set; }
    }
}