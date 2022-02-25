using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs.Order;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View(_orderService.GetUserOrders(User.Identity.Name));
        }

        [Route("ShowOrder/{orderId}")]
        public IActionResult ShowOrder(int orderId,bool isFinalled=false,string discountStatus="")
        {
            var order = _orderService.GetOrderByIdForShow(User.Identity.Name,orderId);
            if(order == null)
            {
                return NotFound();
            }
            ViewBag.IsFinalled=isFinalled;
            ViewBag.DiscountStatus=discountStatus;
            return View(order);
        }
        [Route("FinalOrder/{orderId}")]
        public IActionResult FinalOrder(int orderId)
        {
            if(_orderService.FinalUserOrder(User.Identity.Name, orderId))
            {
                return Redirect("/ShowOrder/" + orderId+ "?isFinalled=true");
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        public IActionResult UseDiscount(string code,int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return View();
            }
            DiscountUseTypes type = _orderService.UseDiscount(code, orderId);

            return Redirect("/ShowOrder/" + orderId + "?discountStatus=" + type);
        }
    }
}
