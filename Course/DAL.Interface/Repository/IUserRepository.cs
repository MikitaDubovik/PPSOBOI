using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetUserByLogin(string login);
        bool CheckIfUserExists(string login);
        void UpdateProfile(int userId, string newName, byte[] newProfile, string ageId, string sexId, string countryId, string languageId);
    }
}
