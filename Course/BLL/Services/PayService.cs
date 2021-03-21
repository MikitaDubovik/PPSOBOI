using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Entities.Ad;
using BLL.Interface.Services;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class PayService : IPayService
    {
        private readonly IPayRepository _payRepository;
        private readonly IPostRepository _postRepository;

        public PayService(IPayRepository payRepository, IPostRepository postRepository)
        {
            _payRepository = payRepository;
            _postRepository = postRepository;
        }

        public List<string> All()
        {
            throw new NotImplementedException();
        }

        public void Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Pay(BllPost post)
        {
            return _payRepository.Pay(Mapper.CreateMap().Map<DalPost>(post));
        }

        public List<BllSex> GetSex()
        {
            return _payRepository.GetSex().Select(p => Mapper.CreateMap().Map<BllSex>(p)).ToList();
        }

        public List<BllAge> GetAges()
        {
            return _payRepository.GetAge().Select(p => Mapper.CreateMap().Map<BllAge>(p)).ToList();
        }

        public List<BllCountry> GetCountries()
        {
            return _payRepository.GetCountries().Select(p => Mapper.CreateMap().Map<BllCountry>(p)).ToList();
        }

        public List<BllLanguage> GetLanguages()
        {
            return _payRepository.GetLanguages().Select(p => Mapper.CreateMap().Map<BllLanguage>(p)).ToList();
        }

        public List<BllPost> GetManagementInfo()
        {
            return _postRepository.GetAllWithAd().Select(p => Mapper.CreateMap().Map<BllPost>(p)).ToList();
        }
    }
}
