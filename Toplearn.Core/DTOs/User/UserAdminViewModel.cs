using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.User
{
    public class UsersForShowInAdminViewModel
    {
        public List<TopLearn.Data.Entities.User.User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
