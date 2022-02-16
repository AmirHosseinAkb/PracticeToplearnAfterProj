using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.User;
using TopLearn.Data.Entities.Wallet;

namespace TopLearn.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            ViewData["UserWalletsList"]=_userService.GetUserWalletsForShow(User.Identity.Name);
            return View();
        }

        [Route("ChargeWallet")]
        [HttpPost]
        public IActionResult ChargeWallet(ChargeUserWalleViewModel charge)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",charge);
            }
            Wallet wallet = new Wallet()
            {
                Amount = charge.Amount,
                Description = "واریز به کیف پول",
                IsPayed = false,
                CreateDate = DateTime.Now,
                TypeId = 1,
                UserId=_userService.GetUserIdByUserName(User.Identity.Name)
            };
            int walletId = _userService.AddWallet(wallet);

            var payment = new ZarinpalSandbox.Payment(charge.Amount);
            var response = payment.PaymentRequest("شارژ کیف پول", "http://localhost:5169/OnlinePayment/" + walletId);
            if (response.Result.Status== 100)
            {
                return Redirect("https://SandBox.Zarinpal.Com/pg/StartPay/" + response.Result.Authority);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
