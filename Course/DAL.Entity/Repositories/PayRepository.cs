using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using ORM.Entity;

namespace DAL
{
    public class PayRepository : IPayRepository
    {
        private readonly ApplicationDbContext _context;

        public PayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DalPost> Get()
        {
            throw new NotImplementedException();
        }

        public DalPost Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Pay(DalPost post)
        {
            try
            {
                _context.Posts.Add(Mapper.CreateMap().Map<Post>(post));
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<DalSex> GetSex()
        {
            return _context.Sex.Select(s => new DalSex {SexId = s.SexId, Label = s.Label});
        }

        public IEnumerable<DalAge> GetAge()
        {
            return _context.Ages.Select(s => new DalAge { AgeId = s.AgeId, Label = s.Label });
        }

        public IEnumerable<DalCountry> GetCountries()
        {
            return _context.Countries.Select(s => new DalCountry { CountryId = s.CountryId, Label = s.Label });
        }

        public IEnumerable<DalLanguage> GetLanguages()
        {
            return _context.Languages.Select(s => new DalLanguage { LanguageId = s.LanguageId, Label = s.Label });
        }
    }
}
