using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace GrigCorePlayer.Commands.Utilities
{
    public static class ExtanstionUtilities
    {
        public static void SetupPagingSupport(RadListBox bListBox, RadDataPager dataPager,
            FrameworkElement uiElement, int PageSize)
        {           
            var radDataBinding = new Binding("ItemsSources");
            radDataBinding.ElementName = uiElement.Name;
            radDataBinding.Mode = BindingMode.TwoWay;
            radDataBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            dataPager.SetBinding(RadDataPager.SourceProperty, radDataBinding);

            var listboxBinding = new Binding("PagedSource");
            listboxBinding.ElementName = dataPager.Name;
            listboxBinding.Mode = BindingMode.TwoWay;
            listboxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bListBox.SetBinding(ItemsControl.ItemsSourceProperty, listboxBinding);

            // Settings
            if (PageSize != 0)
                dataPager.PageSize = PageSize;
        }


        public static void SetupPagingSupport(RadGridView bListBox, RadDataPager dataPager,
            FrameworkElement uiElement, int PageSize)
        {
            var radDataBinding = new Binding("SourceCollection");
            radDataBinding.ElementName = uiElement.Name;
            radDataBinding.Mode = BindingMode.TwoWay;
            radDataBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            dataPager.SetBinding(RadDataPager.SourceProperty, radDataBinding);

            var listboxBinding = new Binding("PagedSource");
            listboxBinding.ElementName = dataPager.Name;
            listboxBinding.Mode = BindingMode.TwoWay;
            listboxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bListBox.SetBinding(ItemsControl.ItemsSourceProperty, listboxBinding);

            // Settings
            if (PageSize != 0)
                dataPager.PageSize = PageSize;
        }


    }
}
