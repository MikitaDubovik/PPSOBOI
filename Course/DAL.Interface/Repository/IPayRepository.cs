using DAL.Interface.DTO;
using System.Collections.Generic;

namespace DAL.Interface.Repository
{
    public interface IPayRepository
    {
        IEnumerable<DalPost> Get();

        DalPost Get(int id);

        bool Pay(DalPost payment);

        IEnumerable<DalSex> GetSex();

        IEnumerable<DalAge> GetAge();

        IEnumerable<DalCountry> GetCountries();

        IEnumerable<DalLanguage> GetLanguages();
    }
}
