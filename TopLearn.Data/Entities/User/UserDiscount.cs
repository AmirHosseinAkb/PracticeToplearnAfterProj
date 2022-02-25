using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.User
{
    public class UserDiscount
    {
        [Key]
        public int UD_Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int DiscountId { get; set; }

        #region Relations

        public User User { get; set; }
        public Order.Discount Discount { get; set; }

        #endregion
    }
}
