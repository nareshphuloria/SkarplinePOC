#region Using directives

using Skarpline.BusinessLayer.Service.User;
using Skarpline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;


#endregion

namespace Skarpline.API.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost]
        [Route("isexist")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> IsExist(string name)
        {
            var userid = await userService.IsExist(name);
            return this.Ok(userid);
        }

        [Route("getall")]
        [ResponseType(typeof(IEnumerable<UserViewModel>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var users = await userService.GetAll();
            return this.Ok(users);
        }

        [HttpPost]
        [Route("update")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Update(UserViewModel user)
        {
            var result = await userService.Update(user);
            return this.Ok(result);
        }
    }
}