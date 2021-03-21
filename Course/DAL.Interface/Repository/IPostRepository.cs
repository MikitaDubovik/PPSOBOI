using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IPostRepository : IRepository<DalPost>
    {
        IEnumerable<DalPost> GetAllWithoutAd(int skip = 0, int take = 10);

        IEnumerable<DalPost> GetAllWithAd();

        IEnumerable<DalPost> GetByTag(string tag, int skip = 0, int take = 10);

        int CountByTag(string tag);

        int CountByUserId(int userId);

        int CountAll();

        IEnumerable<DalPost> GetByUserId(int userId, int skip = 0, int take = 10);

        void LikePost(int postId, int userId);

        void DislikePost(int postId, int userId);

        IEnumerable<string> FindTag(string tag);

        void DeleteAdForUser(int postId, int userId);

        IEnumerable<DalPost> GetDisabledAds(int userId);
    }
}
