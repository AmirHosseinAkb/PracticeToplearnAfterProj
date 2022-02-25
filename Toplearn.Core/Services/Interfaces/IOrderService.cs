using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.Order;
using TopLearn.Core.DTOs.Order;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        Order GetOrderByIdForShow(string userName, int orderId);
        bool FinalUserOrder(string userName, int orderId);
        List<Order> GetUserOrders(string userName);
        Order GetOrderById(int orderId);

        #region Discount
        DiscountUseTypes UseDiscount(string code, int orderId);
        void AddDiscount(Discount discount);
        bool IsExistDiscountCode(string code);
        List<Discount> GetAllDiscounts();
        Discount GetDiscountById(int discountId);
        void UpdateDiscount(Discount discount);
        #endregion

    }
}
