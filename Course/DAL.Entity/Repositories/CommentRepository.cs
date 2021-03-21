using System.Collections.Generic;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM.Context;
using ORM.Entity;

namespace DAL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository()
        {
            _context = new ApplicationDbContext();
        }

        public DalComment GetById(int key)
        {
            return Mapper.CreateMap().Map<DalComment>(_context.Comments.Find(key));
        }

        public void Insert(DalComment entity)
        {
            _context.Comments.Add(Mapper.CreateMap().Map<Comment>(entity));
            _context.SaveChanges();
        }

        public void Delete(DalComment entity)
        {
            var temp = _context.Comments.Find(entity.CommentId);
            _context.Comments.Remove(temp);
            _context.SaveChanges();
        }

        public void Update(DalComment entity)
        {
            var temp = _context.Comments.Find(entity.CommentId);
            temp = Mapper.CreateMap().Map<Comment>(entity);
            _context.SaveChanges();
        }

        public IEnumerable<DalComment> GetByPostId(int postId, int skip = 0, int take = 10)
        {
            return _context.Comments.OrderByDescending(p => p.Posted).Where(p => p.PostId == postId).Skip(skip).
                Take(take).AsEnumerable().Select(c => Mapper.CreateMap().Map<DalComment>(c));
        }

        public void DeleteAllCommentsToPost(int postId)
        {
            _context.Comments.RemoveRange(_context.Comments.Where(i => i.PostId == postId));
            _context.SaveChanges();
        }

        public int CountByPostId(int postId)
        {
            return _context.Comments.Count(i => i.PostId == postId);
        }
    }
}
