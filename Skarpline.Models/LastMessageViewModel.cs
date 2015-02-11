#region Using directives

using System;

#endregion

namespace Skarpline.Models
{
    public class LastMessageViewModel
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public DateTime? MessageTime { get; set; }
    }
}