using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interface.Entities;
using BLL.Interface.Entities.Ad;
using DAL.Interface.DTO;

namespace BLL
{
    public static class Mapper
    {
        public static IMapper CreateMap()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<DalUser, BllUser>().ReverseMap();
                cfg.CreateMap<DalPost, BllPost>().ReverseMap();
                cfg.CreateMap<DalUserLikesEntity, BllUserLikesEntity>().ReverseMap();

                cfg.CreateMap<DalComment, BllComment>().
                    ForMember(x=>x.User, x=>x.MapFrom(y=> new BllUser{UserId = y.UserId}));
                cfg.CreateMap<BllComment, DalComment>().
                    ForMember(x => x.UserId, x => x.MapFrom(y => y.User.UserId));

                cfg.CreateMap<BllAge, DalAge>().ReverseMap();

                cfg.CreateMap<BllCountry, DalCountry>().ReverseMap();

                cfg.CreateMap<BllLanguage, DalLanguage>().ReverseMap();

                cfg.CreateMap<BllSex, DalSex>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
