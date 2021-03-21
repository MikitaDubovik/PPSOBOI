using AutoMapper;
using DAL.Interface.DTO;
using ORM.Entity;

namespace DAL
{
    public static class Mapper
    {
        public static IMapper CreateMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, DalUser>().
                    ForMember(x => x.Roles, y => y.MapFrom(x => x.Roles.Name));
                cfg.CreateMap<DalUser, User>().
                    ForMember(x => x.Roles, y => y.MapFrom(x => new Role { Name = x.Roles }));

                cfg.CreateMap<Post, DalPost>();
                cfg.CreateMap<DalPost, Post>().
                    ForMember(x => x.UserId, y => y.MapFrom(x => x.User.UserId)).
                    ForMember(x => x.User, y => y.UseValue<User>(null));

                cfg.CreateMap<UserLikesEntity, DalUserLikesEntity>().ReverseMap();

                cfg.CreateMap<Comment, DalComment>().ReverseMap();

                cfg.CreateMap<Tag, DalTag>().ReverseMap();

                cfg.CreateMap<Age, DalAge>().ReverseMap();

                cfg.CreateMap <Country, DalCountry> ().ReverseMap();

                cfg.CreateMap <Language, DalLanguage> ().ReverseMap();

                cfg.CreateMap <Sex, DalSex> ().ReverseMap();

                cfg.CreateMap<DisabledAd, DalPost>().
                    ForMember(x => x.PostId, y => y.MapFrom(x => x.PostId)).
                    ForAllOtherMembers(null);
            });

            return config.CreateMapper();
        }
    }
}
