using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure;
using MvcPL.Models;
using MvcPL.Models.Pagination;

namespace MvcPL.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public const int ImagesOnPage = 10;
        private readonly IAccountService _accountService;
        private readonly IPostService _postService;
        private readonly IPayService _payService;
        private static ProfileInfoViewModel _profileModel;

        public ProfileController(IAccountService accountService, IPostService postService, IPayService payService)
        {
            _accountService = accountService;
            _postService = postService;
            _payService = payService;
        }

        public ActionResult Index()
        {
            ProfileViewModel profileViewModel = _accountService.
                GetUserByLogin(User.Identity.Name).ToProfileViewModel();
            int userId = _accountService.GetUserByLogin(User.Identity.Name).UserId;

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = 1,
                PageSize = ImagesOnPage,
                TotalItems = _postService.CountByUserId(userId)
            };
            IEnumerable<PostViewModel> photos = _postService.GetByUserId(userId, 0, ImagesOnPage)
                .Select(p => p.ToPostViewModel());
            ViewBag.Posts = new PaginationViewModel<PostViewModel> { PageInfo = pageInfo, Items = photos };

            return View("Index", profileViewModel);
        }

        public ActionResult LoadMorePosts(int page)
        {
            int userId = _accountService.GetUserByLogin(User.Identity.Name).UserId;

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page + 1,
                PageSize = ImagesOnPage,
                TotalItems = _postService.CountByUserId(userId)
            };
            IEnumerable<PostViewModel> photos = _postService.GetByUserId(userId,
                pageInfo.Skip, pageInfo.PageSize).Select(p => p.ToPostViewModel());

            var model = new PaginationViewModel<PostViewModel> { PageInfo = pageInfo, Items = photos };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowImage()
        {
            return File(_accountService.GetUserByLogin(User.Identity.Name).ProfilePhoto, "image/jpeg");
        }

        [AllowAnonymous]
        public ActionResult ShowProfilePost(int userId)
        {
            return File(_accountService.GetUserById(userId).ProfilePhoto, "image/jpeg");
        }

        public ActionResult UploadPost()
        {
            return View("UploadPost");
        }

        [HttpPost]
        public ActionResult UploadPost(UploadPostViewModel photo)
        {
            _postService.Add(photo.ToBllPost(_accountService.GetUserByLogin(User.Identity.Name).UserId));
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult EditProfile()
        {
            FeelViewBagWithAd();
            ProfileInfoViewModel profileViewModel = _accountService.
                GetUserByLogin(User.Identity.Name).ToProfileInfoViewModel();
            return View("EditProfile", profileViewModel);
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            int userId = _accountService.GetUserByLogin(User.Identity.Name).UserId;

            _accountService.UpdateUserProfile(
                userId,
                model.Name,
                model.ImageFile.ToByteArray(),
                _profileModel.Age,
                _profileModel.Sex,
                _profileModel.Country,
                _profileModel.Language);

            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        public void GetProfileInfo(ProfileInfoViewModel model)
        {
            _profileModel = model;
        }

        [Authorize]
        public ActionResult DeletePost(int postId)
        {
            _postService.Delete(postId);
            return RedirectToAction("Index");
        }

        private void FeelViewBagWithAd()
        {
            var list = new SelectList(_payService.GetCountries(), "CountryId", "Label");
            var sortList = list.OrderBy(p => p.Text).ToList();
            ViewBag.Countries = new SelectList(sortList, "Value", "Text");

            list = new SelectList(_payService.GetSex(), "SexId", "Label");
            sortList = list.OrderBy(p => p.Text).ToList();
            ViewBag.Sex = new SelectList(sortList, "Value", "Text");

            list = new SelectList(_payService.GetAges(), "AgeId", "Label");
            sortList = list.OrderBy(p => p.Text).ToList();
            ViewBag.AgeBegin = new SelectList(sortList, "Value", "Text");

            list = new SelectList(_payService.GetLanguages(), "LanguageId", "Label");
            sortList = list.OrderBy(p => p.Text).ToList();
            ViewBag.Languages = new SelectList(sortList, "Value", "Text");
        }
    }
}