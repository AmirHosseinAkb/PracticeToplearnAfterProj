using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.Order
{
    public  class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int CoursePrice { get; set; }
        [Required]
        public int Count { get; set; }

        #region Relations

        public Order Order { get; set; }
        public Course.Course Course { get; set; }

        #endregion
    }
}
