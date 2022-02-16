using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.User;


namespace TopLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        List<Role> GetAllRoles();
    }
}
