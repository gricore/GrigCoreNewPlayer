using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GrigCorePlayer.Controls.CustomItems;

namespace GrigCorePlayer.Controls.CustomListBox
{
    /// <summary>
    /// Interaction logic for TagsListBox.xaml
    /// </summary>
    public partial class TagsListBox : UserControl
    {
        #region Dependencyes

        public static readonly DependencyProperty ItemsSourcesProperty =
            DependencyProperty.Register("ItemsSources", typeof(TagsListBoxItemsCollection),
            typeof(TagsListBox), new PropertyMetadata(default(TagsListBoxItemsCollection)));

        public TagsListBoxItemsCollection ItemsSources
        {
            get { return (TagsListBoxItemsCollection)GetValue(ItemsSourcesProperty); }
            set { SetValue(ItemsSourcesProperty, value); }
        }

        #endregion

        public static readonly DependencyProperty SelectedNameProperty =
            DependencyProperty.Register("SelectedName", typeof(string), typeof(TagsListBox), new PropertyMetadata(default(string)));

        public string SelectedName
        {
            get { return (string)GetValue(SelectedNameProperty); }
            set { SetValue(SelectedNameProperty, value); }
        }

        public TagsListBox()
        {
            InitializeComponent();
        }

        public delegate void OnTagsListBoxItemMouseUpEventHandler(object e, EventArgs args);

        public event OnTagsListBoxItemMouseUpEventHandler OnTagsListBoxItemMouseUp;

        #region Event Handlers

        private void TagsItem_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            string objName = string.Empty;
            var txtBlock = sender as TextBlock;
            if (txtBlock != null)
            {
                objName = txtBlock.Text;
                SelectedName = objName;
            }

            if (OnTagsListBoxItemMouseUp != null)
                OnTagsListBoxItemMouseUp(objName, e);
        }

        #endregion
    }
}
