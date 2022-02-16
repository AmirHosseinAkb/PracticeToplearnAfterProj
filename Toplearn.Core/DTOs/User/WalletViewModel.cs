using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.User
{
    public class UserWalletsForShowViewModel
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public bool IsPayed { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class ChargeUserWalleViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }
}
