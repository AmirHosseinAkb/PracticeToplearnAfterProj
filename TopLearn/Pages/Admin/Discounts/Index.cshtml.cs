using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Order;

namespace TopLearn.Pages.Admin.Discounts
{
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public List<Discount> Discounts { get; set; }
        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscounts();
        }
    }
}
