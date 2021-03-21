using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM.Context;
using ORM.Entity;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository()
        {
            _context = new ApplicationDbContext();
        }

        public DalUser GetById(int key)
        {
            return Mapper.CreateMap().Map<DalUser>(_context.Users.FirstOrDefault(u => u.UserId == key));
        }

        public void Insert(DalUser entity)
        {
            var tempEntity = Mapper.CreateMap().Map<User>(entity);
            var temp = _context.Roles.FirstOrDefault(r => r.Name.Equals(tempEntity.Roles.Name));
            tempEntity.Roles = temp;
            _context.Users.Add(tempEntity);
            _context.SaveChanges();

        }

        public void Delete(DalUser entity)
        {
            _context.Users.Remove(Mapper.CreateMap().Map<User>(entity));
            _context.SaveChanges();
        }

        public void Update(DalUser entity)
        {
            var temp = _context.Users.Find(entity.UserId);
            temp = Mapper.CreateMap().Map<User>(entity);
            _context.SaveChanges();
        }

        public DalUser GetUserByLogin(string login)
        {
            return Mapper.CreateMap().Map<DalUser>(
                _context.Users.
                    Include(u => u.Roles).
                    FirstOrDefault(u => u.Login == login));
        }

        public bool CheckIfUserExists(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login) != null;
        }

        public void UpdateProfile(int userId, string newName, byte[] newProfile, string ageId, string sexId, string countryId,
            string languageId)
        {
            var temp = _context.Users.Find(userId);

            var tempName = UpdateProperty(temp.Name, newName);

            temp.Name = tempName == null ? string.Empty : tempName.ToString();
            temp.ProfilePhoto = UpdateProperty(temp.ProfilePhoto, newProfile);

            temp.AgeId = UpdateProperty(temp.AgeId, ageId);
            temp.SexId = UpdateProperty(temp.SexId, sexId);
            temp.CountryId = UpdateProperty(temp.CountryId, countryId);
            temp.LanguageId = UpdateProperty(temp.LanguageId, languageId);

            _context.SaveChanges();
        }

        private byte[] UpdateProperty(byte[] oldVal, byte[] newVal)
        {
            if (oldVal.Length != 0 && newVal.Length == 0)
            {
                return oldVal;
            }

            if (oldVal.Length == 0 && newVal.Length == 0)
            {
                return new byte[0];
            }

            return newVal;
        }

        private object UpdateProperty(object oldVal, object newVal)
        {
            if (oldVal != null && newVal == null)
            {
                return oldVal;
            }

            if (oldVal == null && newVal == null)
            {
                return null;
            }

            return newVal;
        }

        private int? UpdateProperty(int? oldVal, string newVal)
        {
            if (oldVal != null && newVal == null)
            {
                return oldVal;
            }

            if (oldVal == null && newVal == null)
            {
                return null;
            }

            return int.Parse(newVal);
        }
    }
}
