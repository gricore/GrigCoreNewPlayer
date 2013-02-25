using System.ComponentModel;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Model
{
    public sealed class HomeModel : INotifyPropertyChanged
    {
        private string _searchBoxText = string.Empty;
        /// <summary>
        /// Gets or sets search box text
        /// </summary>
        public string SearchBoxText
        {
            get { return _searchBoxText; }
            set
            {
                if (_searchBoxText != value)
                {
                    _searchBoxText = value;
                    OnPropertyChanged("SearchBoxText");
                }
            }
        }

        private string _loginText = "Login";
        /// <summary>
        /// Bind login text
        /// </summary>
        public string LoginText
        {
            get { return _loginText; }
            set
            {
                if (_loginText != value)
                {
                    _loginText = value;
                    OnPropertyChanged("LoginText");
                }
            }
        }


        private string _loginPicture = string.Empty;
        /// <summary>
        /// Bind login picture
        /// </summary>
        public string LoginPicture
        {
            get { return _loginPicture; }
            set
            {
                if (_loginPicture != value)
                {
                    _loginPicture = value;
                    OnPropertyChanged("LoginPicture");
                }
            }
        }


        public bool ArtistChecked { get; set; }
        public bool TagChecked { get; set; }


        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
