using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Order;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.Course;
using TopLearn.Data.Entities.Order;
using TopLearn.Data.Entities.User;
using TopLearn.Data.Entities.Wallet;

namespace TopLearn.Core.Services
{
    public class OrderService : IOrderService
    {
        private TopLearnContext _context;
        private IUserService _userService;
        public OrderService(TopLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public void AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public int AddOrder(string userName, int courseId)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            var course = _context.Courses.Find(courseId);
            var order = _context.Orders.SingleOrDefault(o => o.UserId == userId && !o.IsFinally);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    OrderSum = course.CoursePrice,
                    CreateDate = DateTime.Now,
                    IsFinally = false,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId = courseId,
                            Count = 1,
                            CoursePrice = course.CoursePrice
                        }
                    }
                };
                _context.Orders.Add(order);
            }
            else
            {
                var orderDetail = _context.OrderDetails.SingleOrDefault(d => d.OrderId == order.OrderId && d.CourseId == course.CourseId);
                if (orderDetail == null)
                {
                    orderDetail = new OrderDetail()
                    {
                        CourseId = course.CourseId,
                        OrderId = order.OrderId,
                        Count = 1,
                        CoursePrice = course.CoursePrice
                    };
                    _context.OrderDetails.Add(orderDetail);
                }
                else
                {
                    orderDetail.Count++;
                }
                order.OrderSum += course.CoursePrice;
            }
            _context.SaveChanges();
            return order.OrderId;
        }

        public bool FinalUserOrder(string userName, int orderId)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            var order = _context.Orders.Include(o => o.OrderDetails).ThenInclude(d => d.Course).SingleOrDefault(o => o.OrderId == orderId && o.UserId == userId);
            if (order == null || order.IsFinally)
            {
                return false;
            }
            if (_userService.BalanceUserWallet(userName) > order.OrderSum)
            {
                _context.Wallets.Add(new Wallet()
                {
                    UserId = userId,
                    Amount = order.OrderSum,
                    TypeId = 2,
                    Description = "پرداخت فاکتور شماره #" + order.OrderId,
                    CreateDate = DateTime.Now,
                    IsPayed = true
                });
                order.IsFinally = true;
                foreach (var detail in order.OrderDetails)
                {
                    _context.UserCourses.Add(new UserCourse()
                    {
                        UserId = userId,
                        CourseId = detail.CourseId
                    });
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetDiscountById(int discountId)
        {
            return _context.Discounts.Find(discountId);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public Order GetOrderByIdForShow(string userName, int orderId)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Course)
                .SingleOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }

        public List<Order> GetUserOrders(string userName)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public bool IsExistDiscountCode(string code)
        {
            return _context.Discounts.Any(d => d.DiscountCode == code);
        }

        public void UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges();
        }

        public DiscountUseTypes UseDiscount(string code, int orderId)
        {
            var discount = _context.Discounts.SingleOrDefault(d => d.DiscountCode == code);
            if (discount == null)
                return DiscountUseTypes.NotFound;

            if (discount.StartDate != null && discount.StartDate > DateTime.Now)
                return DiscountUseTypes.ExpiredTime;

            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
                return DiscountUseTypes.ExpiredTime;

            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseTypes.Finished;

            var order = GetOrderById(orderId);
            if (_context.UserDiscounts.Any(ud => ud.UserId == order.UserId && ud.DiscountId == discount.DiscountId))
                return DiscountUseTypes.UsedByUser;

            int discountPrice = (order.OrderSum * discount.DiscountPercent) / 100;
            order.OrderSum -= discountPrice;
            if (discount.UsableCount != null)
            {
                discount.UsableCount--;
            }
            _context.UserDiscounts.Add(new UserDiscount()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId
            });
            _context.SaveChanges();
            return DiscountUseTypes.Success;
        }
    }
}
