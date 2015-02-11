#region Using directives

using Skarpline.BusinessLayer.Service.Message;
using Skarpline.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;


#endregion

namespace Skarpline.API.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessagesService messageService;

        public MessageController(IMessagesService _messageService)
        {
            messageService = _messageService;
        }

        [Route("getlastmessages")]
        [ResponseType(typeof(IEnumerable<LastMessageViewModel>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var messages = await messageService.GetAll();
            return this.Ok(messages);
        }

        [HttpPost]
        [Route("save")]
        [ResponseType(typeof(IEnumerable<LastMessageViewModel>))]
        public async Task<IHttpActionResult> Save(string message, int uerId)
        {
            var messageViewModel = new MessagesViewModel();
            messageViewModel.Message = message;
            messageViewModel.MessageTime = DateTime.Now;
            messageViewModel.UserId = uerId;
            var messages = await messageService.Save(messageViewModel);

            return this.Ok(messages);
        }
    }
}