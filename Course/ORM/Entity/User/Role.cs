using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Entity
{
    public class Role
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}