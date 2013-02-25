using GrigCorePlayer.Controllers;
using Microsoft.Practices.Unity;
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

namespace GrigCorePlayer.Views.Station
{
    /// <summary>
    /// Interaction logic for StationView.xaml
    /// </summary>
    public partial class StationView : Page
    {
        private readonly IUnityContainer _container;

        public StationView(IUnityContainer container)
        {
            _container = container;
            InitializeComponent();

            DataContext = _container.Resolve<StationController>();
        }
    }
}
