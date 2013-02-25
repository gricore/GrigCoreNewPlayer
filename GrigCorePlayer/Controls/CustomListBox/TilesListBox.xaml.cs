using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GrigCorePlayer.Commands.Utilities;
using GrigCorePlayer.Controls.CustomItems;
using System;
using Telerik.Windows.Controls;
using System.Windows.Data;

namespace GrigCorePlayer.Controls.CustomListBox
{
    /// <summary>
    /// Interaction logic for TilesListBox.xaml
    /// </summary>
    public partial class TilesListBox : UserControl
    {
        public TilesListBox()
        {
            InitializeComponent();

            //ExtanstionUtilities.SetupPagingSupport(TheListBox, radDataPager, this, 0);
        }

        #region Events

        public delegate void SelectedChangedEventHandler(object sender, SelectionChangedEventArgs e);
        public event SelectedChangedEventHandler SelectedChanged;

        public delegate void ItemDoubleClickedEventHandler(object sender, MouseButtonEventArgs e);
        public event ItemDoubleClickedEventHandler ItemDoubleClicked;

        #endregion

        #region SelectedIndex
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int),
            typeof(TilesListBox), new PropertyMetadata(default(int)));


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        #endregion

        #region Selected Item

        public static readonly DependencyProperty SelectedItemProperty =
           DependencyProperty.Register("SelectedItem", typeof(object),
           typeof(TilesListBox), new PropertyMetadata(default(object)));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        #region Sources       

        public static readonly DependencyProperty ItemsSourcesProperty = DependencyProperty.Register("ItemsSources", typeof(TilesListBoxItemSources), typeof(TilesListBox), new PropertyMetadata(default(TilesListBoxItemSources)));
        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(object), typeof(TilesListBox), new PropertyMetadata(default(object)));       

        public TilesListBoxItemSources ItemsSources
        {
            get { return (TilesListBoxItemSources)GetValue(ItemsSourcesProperty); }
            set { SetValue(ItemsSourcesProperty, value); }
        }

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        #endregion

        #region Event Handlers

        private bool _isDoubleClick = false;

        private void TheListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = SelectedItem as TilesListBoxItem;
            if (item != null)
            {
                SelectedIndex = item.Id - 1;
            }

            if (SelectedChanged != null)
                SelectedChanged(sender, e);

            if (ItemDoubleClicked != null && _isDoubleClick)
            {
                _isDoubleClick = false;
                ItemDoubleClicked(sender, null);
            }

        }

        private void TilesListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2) return;
            _isDoubleClick = true;
            TheListBox_OnSelectionChanged(sender, null);
            //if (ItemDoubleClicked != null)
            //    ItemDoubleClicked(sender, e);
        }

        #endregion

    }
}
