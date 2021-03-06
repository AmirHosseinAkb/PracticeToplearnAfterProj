using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.User;

namespace TopLearn.Data.Entities.Order
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        [Required]
        [MaxLength(50)]
        public string DiscountCode { get; set; }
        [Required]
        public int DiscountPercent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<UserDiscount>? UserDiscounts { get; set; }
    }
}
