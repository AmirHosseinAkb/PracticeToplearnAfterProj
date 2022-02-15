using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs.User ;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.User;
using TopLearn.Core.Convertors;
using TopLearn.Core.Generators;
using TopLearn.Core.Security;
using TopLearn.Core.Senders;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TopLearn.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRenderService;  
        public AccountController(IUserService userService,IViewRenderService renderService)
        {
            _userService = userService;
            _viewRenderService = renderService;
        }

        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUserViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userService.IsExistUserByUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری از قبل وجود دارد");
                return View(register);
            }
            if (_userService.IsExistUserByEmail(register.Email))
            {
                ModelState.AddModelError("Email", "این ایمیل از قبل وجود دارد");
                return View(register);
            }

            User user = new User()
            {
                UserName = register.UserName,
                Email = EmailConvertor.FixEmail(register.Email),
                Password = PasswordHasher.HashPasswrodMD5(register.Password),
                ActiveCode = NameGenerator.GenerateUniqName(),
                IsActive = false,
                RegisterDate = DateTime.Now,
                AvatarName = "Default.png",
                IsDeleted = false,
            };
            //Add User
            _userService.AddUser(user);
            //Send Email
            var body = _viewRenderService.RenderToStringAsync("_RegistrationEmailView", user);
            SendEmail.Send(user.Email, "تاپلرن|فعالسازی حساب کاربری", body);
            return View("_SuccessRegister", user);
        }
        #endregion

        #region ActiveAccount
        [Route("ActiveUserAccount/{activeCode}")]
        public IActionResult ActiveUserAccount(string activeCode)
        {
            ViewBag.IsActive = _userService.ActiveUserAccount(activeCode);
            return View();
        }
        #endregion

        #region Login&Logout
        [Route("Login")]
        public IActionResult Login(bool IsUserProfileEdited=false)
        {
            ViewBag.IsEdited = IsUserProfileEdited;
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginUserViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userService.GetUserForLogin(login.Email, login.Password);
            if (user != null)
            {
                if (user.IsActive)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsLogedIn = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد لطفا قبل از ورود حساب کاربری خود را فعال کنید");
                    return View(login);
                }
            }
            else
            {
                ModelState.AddModelError("Email","کاربری با این مشخصات وجود ندارد");
                return View(login);
            }
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion

        #region ForgetPassword
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordViewModel forget)
        {
            if (!ModelState.IsValid)
            {
                return View(forget);
            }
            var user = _userService.GetUserByEmail(forget.Email);
            if (user==null)
            {
                ModelState.AddModelError("Email", "کاربری با ایمیل وارد شده وجود ندارد");
                return View(forget);
            }
            var body = _viewRenderService.RenderToStringAsync("_ResetPasswordView", user);
            SendEmail.Send(user.Email, "تاپلرن|بازیابی رمز عبور", body);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion

        #region ResetPassword
        [Route("ResetPassword/{activeCode?}")]
        public IActionResult ResetPassword(string activeCode)
        {
            var resetPasswordVM = new ResetPasswordViewModel()
            {
                ActiveCode = activeCode
            };
            if (!_userService.IsExistUserByActiveCode(activeCode))
            {
                ViewBag.IsUsed = true;
                return View();
            }
            return View(resetPasswordVM);
        }
        [Route("ResetPassword/{activeCode?}")]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }
            ViewBag.IsReseted=_userService.ResetUserPassword(reset);
            return View();
        }
        #endregion

    }
}
