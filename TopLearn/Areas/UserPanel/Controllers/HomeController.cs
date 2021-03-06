using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using TopLearn.Core.Security;

namespace TopLearn.Areas.UserPanel.Controllers
{
    [Area("USerPanel")]
    [Authorize]
    public class HomeController : Controller
    {

        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var user = _userService.GetUserInformationsForShow(User.Identity.Name);
            return View(user);
        }

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            var user = _userService.GetUserInformationsForEdit(User.Identity.Name);
            return View(user);
        }
        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditUserProfileViewModel edit)
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                return View(edit);
            }
            if (edit.UserName != user.UserName)
            {
                if (_userService.IsExistUserByUserName(edit.UserName))
                {
                    ModelState.AddModelError("UserName", "این نام کاربری از قبل وجود دارد");
                    return View(edit);
                }
            }
            if (edit.Email != user.Email)
            {
                if (_userService.IsExistUserByEmail(edit.Email))
                {
                    ModelState.AddModelError("Email", "این ایمیل از قبل وجود دارد");
                    return View(edit);
                }
            }
            _userService.EditUserProfile(User.Identity.Name, edit);
            return Redirect("/Login?IsUserProfileEdited=true");
        }

        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                return View(change);
            }
            if (user.Password != PasswordHasher.HashPasswrodMD5(change.CurrentPassword))
            {
                ModelState.AddModelError("CurrentPassword", "رمز عبور فعلی را بصورت صحیح وارد کنید");
                return View(change);
            }
            user.Password = PasswordHasher.HashPasswrodMD5(change.NewPassword);
            _userService.UpdateUser(user);
            ViewBag.IsChanged = true;
            return View();
        }
    }
}
