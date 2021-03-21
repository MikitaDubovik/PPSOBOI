namespace DAL.Interface.DTO
{
    public class DalUser
    {
        public int UserId { get; set; }

        public string Roles { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public byte[] ProfilePhoto { get; set; }

        public int? AgeId { get; set; }

        public int? CountryId { get; set; }

        public int? LanguageId { get; set; }

        public int? SexId { get; set; }
    }
}
