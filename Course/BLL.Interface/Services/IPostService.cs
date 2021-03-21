using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IPostService
    {
        void Add(BllPost photo);

        IEnumerable<string> FindTags(string tag);

        int CountByTag(string tag);

        IEnumerable<BllPost> GetByTag(string tag, int skip, int take);

        IEnumerable<BllPost> GetAllWithoutAd(int skip, int take);

        IEnumerable<BllPost> GetAllWithAd();

        BllPost GetById(int id);

        IEnumerable<BllPost> GetByUserId(int userId, int skip, int take);

        int CountByUserId(int id);

        void LikePost(int userId, int postId);

        void DislikePost(int userId, int postId);

        void AddComment(BllComment comment);

        int CountCommentByPostId(int postId);

        IEnumerable<BllComment> GetCommentsByPostId(int postId, int skip, int take);

        void Delete(int postId);

        BllUser GetAuthorById(int id);

        void DeleteAdForUser(int postId, int userId);

        IEnumerable<BllPost> GetDisabledAds(int userId);
    }
}
