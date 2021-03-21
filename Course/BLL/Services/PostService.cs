using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.DTO;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceBusConnector _serviceBusConnector;

        public PostService(IPostRepository repository, ICommentRepository commentRepository, IUserRepository userRepository, IServiceBusConnector serviceBusConnector)
        {
            _postRepository = repository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _serviceBusConnector = serviceBusConnector;
        }

        public BllUser GetAuthorById(int id)
        {
            return Mapper.CreateMap().Map<BllUser>(_userRepository.GetById(id));
        }

        public void DeleteAdForUser(int postId, int userId)
        {
            _postRepository.DeleteAdForUser(postId, userId);
        }

        public IEnumerable<BllPost> GetDisabledAds(int userId)
        {
            return _postRepository.GetDisabledAds(userId).Select(p => Mapper.CreateMap().Map<BllPost>(p));
        }

        public IEnumerable<BllPost> GetAllWithoutAd(int skip, int take)
        {
            return _postRepository.GetAllWithoutAd(skip, take).Select(p => Mapper.CreateMap().Map<BllPost>(p));
        }

        public IEnumerable<BllPost> GetAllWithAd()
        {
            return _postRepository.GetAllWithAd().Select(p => Mapper.CreateMap().Map<BllPost>(p));
        }

        public void Add(BllPost photo)
        {
            _postRepository.Insert(Mapper.CreateMap().Map<DalPost>(photo));
            Task.Run(() => _serviceBusConnector.SendImage(photo))
            ;
        }

        public IEnumerable<string> FindTags(string tag)
        {
            return _postRepository.FindTag(tag);
        }

        public int CountByTag(string tag)
        {
            if (tag == string.Empty)
                return _postRepository.CountAll();
            return _postRepository.CountByTag(tag);
        }

        public IEnumerable<BllPost> GetByTag(string tag, int skip, int take)
        {
            if (tag == string.Empty)
            {
                return _postRepository.GetAllWithoutAd(skip, take).Select(p => Mapper.CreateMap().Map<BllPost>(p));
            }

            return _postRepository.GetByTag(tag, skip, take).Select(p => Mapper.CreateMap().Map<BllPost>(p));
        }

        public BllPost GetById(int id)
        {
            return Mapper.CreateMap().Map<BllPost>(_postRepository.GetById(id));
        }

        public IEnumerable<BllPost> GetByUserId(int userId, int skip, int take)
        {
            return _postRepository.GetByUserId(userId, skip, take).Select(p => Mapper.CreateMap().Map<BllPost>(p));
        }

        public int CountByUserId(int id)
        {
            return _postRepository.CountByUserId(id);
        }

        public void LikePost(int userId, int postId)
        {
            _postRepository.LikePost(postId, userId);
        }

        public void DislikePost(int userId, int postId)
        {
            _postRepository.DislikePost(postId, userId);
        }

        public void AddComment(BllComment comment)
        {
            _commentRepository.Insert(Mapper.CreateMap().Map<DalComment>(comment));
        }

        public int CountCommentByPostId(int postId)
        {
            return _commentRepository.CountByPostId(postId);
        }

        public IEnumerable<BllComment> GetCommentsByPostId(int postId, int skip, int take)
        {
            return _commentRepository.GetByPostId(postId, skip, take).Select(p => Mapper.CreateMap().Map<BllComment>(p));
        }

        public void Delete(int postId)
        {
            _commentRepository.DeleteAllCommentsToPost(postId);
            _postRepository.Delete(_postRepository.GetById(postId));
        }
    }
}
