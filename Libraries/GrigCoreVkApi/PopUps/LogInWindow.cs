using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GrigCoreVkApi.PopUps
{
    public class LogInWindow : Window
    {
        public LogInWindow(WebBrowser WBroser)
        {
            this.Width = 500;
            this.Height = 400;
            this.Title = "LogIn to Last.fm";
            this.AddChild(WBroser);
        }
    }
}
