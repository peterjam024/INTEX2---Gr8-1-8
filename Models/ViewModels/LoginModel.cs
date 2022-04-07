using System;
using System.ComponentModel.DataAnnotations;

namespace CrashySmashy.Models.ViewModels
{
    //This is where they get the information to login. The username and password are required, and the return URL is not.
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        //this will save the pageURL and send them to wherever they were trying to go!
        public string ReturnUrl { get; set; }
    }
}
