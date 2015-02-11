#region Using directives

using Skarpline.BusinessLayer.Service.User;
using Skarpline.CommonLayer.CommonLibrary;
using Skarpline.CommonLayer.Mapper;
using Skarpline.Models;
using Skarpline.PersistenceLayer.Repository.Entities;
using Skarpline.PersistenceLayer.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Skarpline.BusinessLayer.ServiceImpl.Users
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUnitOfWorkAsync unitOfWork;

        public UserServiceImpl(IUnitOfWorkAsync unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        async Task<int> IUserService.IsExist(string username)
        {
            var loginusers = await unitOfWork.GetRepositoryAsync<User>().GetListAsync(item => item.IsLoggedIn);
            int returnValue = -1;

            ////Check If the limit for maximum allowed users has reached
            if (loginusers.Count >= AppConstants.MaxUsersAllowed)
                returnValue = -1;
            else
            {
                var user = await unitOfWork.GetRepositoryAsync<User>().GetSingleAsync(item =>
                    item.Username.ToLower().Equals(username.ToLower()));

                ////Check If user exist in database
                if (user != null)
                {
                    if (user.IsLoggedIn)
                        returnValue = 0;
                    else
                    {
                        user.IsLoggedIn = true;
                        user.LoggedInAt = DateTime.Now;
                        unitOfWork.GetRepositoryAsync<User>().Update(user);
                        await unitOfWork.SaveChangesAsync();
                        returnValue = user.Id;
                    }
                }
                else
                {
                    user = new User();
                    user.Username = username;
                    user.IsLoggedIn = true;
                    user.LoggedInAt = DateTime.Now;

                    unitOfWork.GetRepositoryAsync<User>().Save(user);
                    await unitOfWork.SaveChangesAsync();
                    returnValue = user.Id;
                }
            }
            return returnValue;
        }

        async Task<UserViewModel> IUserService.GetBy(int id)
        {
            var user = new UserViewModel();

            ObjectMapper.Map(await unitOfWork.GetRepositoryAsync<Skarpline.PersistenceLayer.Repository.Entities.User>().FindAsync(id), user);
            return user;
        }

        async Task<bool> IUserService.Update(UserViewModel user)
        {
            var userEntity = new User();
            ObjectMapper.Map(user, userEntity);

            unitOfWork.GetRepositoryAsync<Skarpline.PersistenceLayer.Repository.Entities.User>().Update(userEntity);
            await unitOfWork.SaveChangesAsync();

            return user.Id > 0;
        }

        async Task<IEnumerable<UserViewModel>> IUserService.GetAll()
        {
            var users = new List<UserViewModel>();
            ObjectMapper.Map(await unitOfWork.GetRepositoryAsync<User>().GetListAsync(item => item.IsLoggedIn), users);

            return users;
        }
    }
}