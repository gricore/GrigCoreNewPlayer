using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCorePlayer.Model
{
    public enum LoginCommandType
    {
        Logout
    }

    public class LoginModel
    {
        public LoginCommandType LoginCommand { get; set; }
    }
}
