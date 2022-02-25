using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Order;

namespace TopLearn.Pages.Admin.Discounts
{
    public class EditDiscountModel : PageModel
    {
        private IOrderService _orderService;
        public EditDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public Discount Discount { get; set; }
        public void OnGet(int discountId)
        {
            Discount=_orderService.GetDiscountById(discountId);
        }

        public IActionResult OnPost(string stDate="",string edDate = "")
        {
            if (stDate != "")
            {
                string[] splitStartDate = stDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(splitStartDate[0]),
                    int.Parse(splitStartDate[1]),
                    int.Parse(splitStartDate[2]),
                    new PersianCalendar());
            }
            if (edDate != "")
            {
                string[] splitEndDate = edDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(splitEndDate[0]),
                    int.Parse(splitEndDate[1]),
                    int.Parse(splitEndDate[2]),
                    new PersianCalendar());
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Update Discount
            _orderService.UpdateDiscount(Discount);
            return RedirectToPage("Index");
        }
    }
}
