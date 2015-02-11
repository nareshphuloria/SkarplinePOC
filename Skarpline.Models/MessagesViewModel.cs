#region Using directives

using System;


#endregion

namespace Skarpline.Models
{
    public class MessagesViewModel
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime? MessageTime { get; set; }
    }
}
