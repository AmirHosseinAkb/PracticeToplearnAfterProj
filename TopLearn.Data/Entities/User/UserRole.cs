using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.User
{
    public  class UserRole
    {
        [Key]
        public int UR_Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }

        #region Relations

        public User User { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}
