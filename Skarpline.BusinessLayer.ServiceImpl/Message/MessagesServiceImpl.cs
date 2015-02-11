#region Using directives

using Skarpline.BusinessLayer.Service.Message;
using Skarpline.CommonLayer.CommonLibrary;
using Skarpline.CommonLayer.Mapper;
using Skarpline.Models;
using Skarpline.PersistenceLayer.Repository.Entities;
using Skarpline.PersistenceLayer.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace Skarpline.BusinessLayer.ServiceImpl.Message
{
    public class MessagesServiceImpl : IMessagesService
    {
        private readonly IUnitOfWorkAsync unitOfWork;

        public MessagesServiceImpl(IUnitOfWorkAsync unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        async Task<bool> IMessagesService.Save(MessagesViewModel message)
        {
            var messageEntity = new Messages();
            ObjectMapper.Map(message, messageEntity);

            unitOfWork.GetRepositoryAsync<Messages>().Save(messageEntity);
            await unitOfWork.SaveChangesAsync();

            return messageEntity.Id > 0;
        }

        async Task<IEnumerable<LastMessageViewModel>> IMessagesService.GetAll()
        {
            var messages = await (from message in unitOfWork.GetRepositoryAsync<Messages>().GetList()
                                  join user in unitOfWork.GetRepositoryAsync<User>().GetList()
                                  on message.UserId equals user.Id
                                  select new LastMessageViewModel
                                  {
                                      Id = message.Id,
                                      Message = message.Message,
                                      MessageTime = message.MessageTime,
                                      UserId = message.UserId,
                                      UserName = user.Username
                                  }).OrderByDescending(item => item.MessageTime).Take(AppConstants.MessageToShowInChatPanel).ToListAsync();

            return messages.OrderBy(item => item.MessageTime);
        }
    }
}