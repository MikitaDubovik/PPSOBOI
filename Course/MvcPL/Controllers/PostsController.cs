using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Helper;
using MvcPL.Infrastructure;
using MvcPL.Models;
using MvcPL.Models.Pagination;

namespace MvcPL.Controllers
{
    public class PostsController : Controller
    {
        public const int ImagesOnPage = 6;
        public const int CommentOnPage = 3;

        private readonly IPostService _postService;
        private readonly IAccountService _accountService;

        public PostsController(IPostService postService, IAccountService accountService)
        {
            _postService = postService;
            _accountService = accountService;
        }

        public ActionResult Index(int page = 1)
        {
            var imagesOnPage = ImagesOnPage;
            var ad = GetAd(ref imagesOnPage);

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = ImagesOnPage,
                TotalItems = _postService.CountByTag(string.Empty),
                UrlPart = Url.Action("LoadMore", "Posts", new { tag = "" })
            };

            IEnumerable<int> photosIds = _postService.GetAllWithoutAd(0, imagesOnPage)
                .Select(p => p.PostId);

            var photos = new List<ImageViewModel>(photosIds.Count());
            photos.AddRange(photosIds.Select(id => new ImageViewModel
            {
                ImageUrl = ToImageUrl(id),
                ImageDetailsUrl = ToImageDetailsUrl(id)
            }));

            if (ad != null)
            {
                photos.Insert(0, new ImageViewModel
                {
                    ImageUrl = ToImageUrl(ad.PostId),
                    ImageDetailsUrl = ToImageDetailsUrl(ad.PostId),
                    IsAd = true
                });
            }

            return View(new PaginationViewModel<ImageViewModel> { Items = photos, PageInfo = pageInfo });
        }

        public ActionResult Find(string term)
        {
            IEnumerable<string> tags = _postService.FindTags(term);

            var projection = from t in tags
                             select new
                             {
                                 label = t,
                                 value = t
                             };
            if (Request.IsAjaxRequest())
            {
                return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
            }
            return View("Index");
        }

        public ActionResult Search(string tag = "", int page = 1)
        {
            var imagesOnPage = ImagesOnPage;
            BllPost ad;

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = ImagesOnPage,
                TotalItems = _postService.CountByTag(tag),
                UrlPart = Url.Action("LoadMore", "Posts", new { tag })
            };

            var photosIds = _postService.GetByTag(tag, pageInfo.Skip, pageInfo.PageSize)
                .Select(p => p.PostId);
            var photos = new List<ImageViewModel>(photosIds.Count());
            photos.AddRange(photosIds.Select(id => new ImageViewModel
            {
                ImageUrl = ToImageUrl(id),
                ImageDetailsUrl = ToImageDetailsUrl(id)
            }));

            var pagedPosts =
                new PaginationViewModel<ImageViewModel> { PageInfo = pageInfo, Items = photos };

            return Json(pagedPosts, JsonRequestBehavior.AllowGet);

        }

        public ActionResult LoadMore(string tag = "", int page = 1)
        {
            var imagesOnPage = ImagesOnPage;
            var ad = GetAd(ref imagesOnPage);

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = ImagesOnPage,
                TotalItems = _postService.CountByTag(tag),
                UrlPart = Url.Action("LoadMore", "Posts", new { tag })
            };

            var photosIds = _postService.GetByTag(tag, pageInfo.Skip, imagesOnPage)
                .Select(p => p.PostId);

            var photos = new List<ImageViewModel>(photosIds.Count());
            photos.AddRange(photosIds.Select(id => new ImageViewModel
            {
                ImageUrl = ToImageUrl(id),
                ImageDetailsUrl = ToImageDetailsUrl(id)
            }));

            if (ad != null)
            {
                photos.Insert(0, new ImageViewModel
                {
                    ImageUrl = ToImageUrl(ad.PostId),
                    ImageDetailsUrl = ToImageDetailsUrl(ad.PostId)
                });
            }

            var pagedPosts =
                new PaginationViewModel<ImageViewModel> { PageInfo = pageInfo, Items = photos };

            return Json(pagedPosts, JsonRequestBehavior.AllowGet);
        }

        private BllPost GetAd(ref int imagesOnPage)
        {
            BllPost ad;
            AdHelper.Initialize(_postService.GetAllWithAd());
            if (User.Identity.IsAuthenticated)
            {
                var user = _accountService.GetUserByLogin(User.Identity.Name);
                var disabledAds = _postService.GetDisabledAds(user.UserId).ToList();
                ad = AdHelper.GetAd(disabledAds, user.AgeId, user.SexId, user.CountryId, user.LanguageId);
                imagesOnPage -= 1;
            }
            else
            {
                ad = AdHelper.GetRandomAd();
            }

            return ad;
        }

        private string ToImageUrl(int id)
        {
            return Url.Action("ShowImage", "Posts", new { id });
        }

        private string ToImageDetailsUrl(int id)
        {
            return Url.Action("PostDetails", "Posts", new { id });
        }


        public ActionResult ShowImage(int id)
        {
            return File(_postService.GetById(id).Image, "image/jpeg");
        }

        public ActionResult PostDetails(int id)
        {
            var photo = _postService.GetById(id).ToPostDetailsViewModel();
            photo.Owner = _accountService.GetUserById(photo.UserId).ToPostOwnerViewModel();
            if (Request.IsAuthenticated)
            {
                photo.CurrentUserId = _accountService.GetUserByLogin(User.Identity.Name).UserId;
            }

            var pageInfo = new PageInfo
            {
                PageNumber = 1,
                PageSize = CommentOnPage,
                TotalItems = _postService.CountCommentByPostId(id)
            };

            var comments = _postService.GetCommentsByPostId(id, 0, CommentOnPage)
                .Select(p => p.ToCommentViewModel()).ToList();

            foreach (var comment in comments)
            {
                comment.Author.Name = _postService.GetAuthorById(comment.Author.Id).Login;
            }

            ViewBag.Comments = new PaginationViewModel<CommentViewModel> { PageInfo = pageInfo, Items = comments };

            photo.UserLikesLogins = new List<string>();

            foreach (var userId in photo.UserLikesIds)
            {
                photo.UserLikesLogins.Add(_postService.GetAuthorById(userId).Login);
            }

            return PartialView("_PostDetails", photo);
        }

        [Authorize]
        public ActionResult LikePost(int postId)
        {
            int userId = _accountService.GetUserByLogin(User.Identity.Name).UserId;
            _postService.LikePost(userId, postId);
            PostRatingViewModel photo = _postService.GetById(postId).ToPostRatingViewModel();
            return PartialView("_LikePhoto", photo);
        }

        [Authorize]
        public ActionResult DislikePost(int postId)
        {
            int userId = _accountService.GetUserByLogin(User.Identity.Name).UserId;
            _postService.DislikePost(userId, postId);
            PostRatingViewModel photo = _postService.GetById(postId).ToPostRatingViewModel();
            return PartialView("_DislikePhoto", photo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            int userId = _accountService.GetUserByLogin(User.Identity.Name).UserId;
            _postService.AddComment(model.ToBllComment(userId, User.Identity.Name));
            return RedirectToAction("LoadMoreComment", new { page = 0, id = model.PostId });
        }

        public ActionResult LoadMoreComment(int page, int id)
        {
            var pageInfo = new PageInfo
            {
                PageNumber = page + 1,
                PageSize = CommentOnPage,
                TotalItems = _postService.CountCommentByPostId(id)
            };

            IEnumerable<CommentViewModel> comments = _postService.GetCommentsByPostId(id,
                pageInfo.Skip, pageInfo.PageSize).Select(p => p.ToCommentViewModel()).ToList();

            foreach (var comment in comments)
            {
                comment.Author.Name = _postService.GetAuthorById(comment.Author.Id).Login;
            }

            var model = new PaginationViewModel<CommentViewModel> { PageInfo = pageInfo, Items = comments };
            return PartialView("_Comments", model);
        }

        public ActionResult DeleteAddPost(string postUrl)
        {
            var postId = postUrl.Split('/').Last();
            var user = _accountService.GetUserByLogin(User.Identity.Name);
            _postService.DeleteAdForUser(int.Parse(postId), user.UserId);
            return RedirectToAction("Index");
        }
    }
}