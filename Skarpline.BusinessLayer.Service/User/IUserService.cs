#region Using directives

using Skarpline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Skarpline.BusinessLayer.Service.User
{
    public interface IUserService
    {
        Task<int> IsExist(string usename);
        Task<UserViewModel> GetBy(int id);
        Task<bool> Update(UserViewModel user);
        Task<IEnumerable<UserViewModel>> GetAll();
    }
}