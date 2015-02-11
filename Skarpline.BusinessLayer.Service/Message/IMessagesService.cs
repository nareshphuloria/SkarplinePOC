#region Using directives

using Skarpline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Skarpline.BusinessLayer.Service.Message
{
    public interface IMessagesService
    {
        Task<bool> Save(MessagesViewModel message);
        Task<IEnumerable<LastMessageViewModel>> GetAll();
    }
}