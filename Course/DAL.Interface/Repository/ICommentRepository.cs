using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface ICommentRepository : IRepository<DalComment>
    {
        IEnumerable<DalComment> GetByPostId(int postId, int skip = 0, int take = 10);
        void DeleteAllCommentsToPost(int postId);
        int CountByPostId(int postId);
    }
}
