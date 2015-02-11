#region Using directives

using System;

#endregion

namespace Skarpline.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool? IsLoggedIn { get; set; }
        public DateTime? LoggedInAt { get; set; }
    }
}
