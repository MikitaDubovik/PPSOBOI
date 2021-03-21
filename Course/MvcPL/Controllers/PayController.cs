using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Helper;
using MvcPL.Infrastructure;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayService _payService;
        private readonly IAccountService _accountService;

        public PayController(IPayService payService, IAccountService accountService)
        {
            _payService = payService;
            _accountService = accountService;
        }

        public ActionResult MakeAd()
        {
            return View("MakeAd");
        }

        [HttpPost]
        public ActionResult MakeAd(UploadAdViewModel post)
        {
            FeelViewBagWithAd();
            PayModel.Model = post;
            PayModel.Model.Photo = post.ImageFile.ToByteArray();
            return View("MakeAdFeatures", post);
        }

        [HttpGet]
        public ActionResult PayPage()
        {
            return View("PayPage", model: PayModel.Model.Price);
        }

        public ActionResult DoPay()
        {
            _payService.Pay(PayModel.Model.ToBllPost(_accountService.GetUserByLogin(User.Identity.Name).UserId));
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult Management()
        {
            var managementViewModel = _payService.GetManagementInfo();
            var model = new AdsAggregatorViewModel
                { ViewModels = managementViewModel.
                    Select(m => new ManagementViewModel
                        { PostId = m.PostId.ToString(),
                            Price = m.Price,
                            UserEmail = m.User.Email

                        }) };

            return View("Management", model);
        }

        [HttpPost]
        public void PostPrice(UploadAdViewModel model)
        {
            PayModel.Model.Price = model.Price;
            PayModel.Model.Language = model.Language;
            PayModel.Model.Age = model.Age;
            PayModel.Model.Countries = model.Countries;
            PayModel.Model.Sex = model.Sex;
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