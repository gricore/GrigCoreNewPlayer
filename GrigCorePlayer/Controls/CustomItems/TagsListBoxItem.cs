using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Controls.CustomItems
{
    public class TagsListBoxItem : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        /// <summary>
        /// Bind title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TagsListBoxItemsCollection : ObservableCollection<TagsListBoxItem>, ICloneable
    {
        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var sources = new TagsListBoxItemsCollection();
            foreach (var item in this)
            {
                sources.Add(item);
            }
            return sources;
        }
    }
}
