using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using MvcPL.Models;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult SignIn(string returnUrl, bool? fancybox)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (fancybox == true)
                return PartialView("_SignIn");
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Login, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Posts");
                }

                ModelState.AddModelError("", "Incorrect login or password.");
            }
            return View("SignIn", viewModel);
        }

        [HttpGet]
        public ActionResult SignUp(string returnUrl, bool? fancybox)
        {
            if (fancybox == true)
                return PartialView("_SignUp");
            return View("SignUp");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (_accountService.CheckIfUserExists(model.Login))
            {
                ModelState.AddModelError("", "User with this login already registered.");
                return View("SignUp", model);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(model.Login, model.Email, model.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    return RedirectToAction("Index", "Posts");
                }

                ModelState.AddModelError("", "Error registration.");
            }
            return View("SignUp", model);
        }

        public ActionResult ValidateSignUp(string login)
        {
            if (_accountService.CheckIfUserExists(login))
                return Json("login is already taken", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateSignIn(string login)
        {
            if (!_accountService.CheckIfUserExists(login))
                return Json("there's no users with such login", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Posts");
        }
    }
}