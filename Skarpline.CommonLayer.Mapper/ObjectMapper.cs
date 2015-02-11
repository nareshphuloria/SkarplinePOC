// -----------------------------------------------------------------------
// <copyright file="ObjectMapper.cs" company="Logiciells">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

#region Using directives

using Skarpline.Models;
using Skarpline.PersistenceLayer.Repository.Entities;

#endregion

namespace Skarpline.CommonLayer.Mapper
{
    public class ObjectMapper : IObjectMapper
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<UserViewModel, User>();
            AutoMapper.Mapper.CreateMap<User, UserViewModel>();

            AutoMapper.Mapper.CreateMap<MessagesViewModel, Messages>();
            AutoMapper.Mapper.CreateMap<Messages, MessagesViewModel>();
        }

        #region Map Object

        public static void Map<T, U>(T sourceObject, U destObject)
        {
            AutoMapper.Mapper.Map(sourceObject, destObject);
        }

        #endregion
    }
}