using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Model
{
    public class HomeFrameworkModel : INotifyPropertyChanged
    {
        private FrameworkElement _loginContent = new FrameworkElement();

        public FrameworkElement LoginContent
        {
            get { return _loginContent; }
            set
            {
                if (!Equals(_loginContent, value))
                {
                    _loginContent = value;
                    OnPropertyChanged("LoginContent");
                }
            }
        }


        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
