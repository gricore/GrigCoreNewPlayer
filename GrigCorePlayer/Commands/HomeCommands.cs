using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Model;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using GrigCorePlayer.Services;
using GrigCorePlayer.Views.Artist;

namespace GrigCorePlayer.Commands
{
    [UsedImplicitly]
    public class SearchBoxKeyDownCommandT : ICommand
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFrameNavigationService _navigationService;
        private readonly IRegionManager _regionManager;

        public SearchBoxKeyDownCommandT(IEventAggregator eventAggregator, IFrameNavigationService navigationService,
            IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _regionManager = regionManager;
        }

        public bool CanExecute(object parameter)
        {
            return Keyboard.PrimaryDevice.IsKeyDown(Key.Enter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var searchboxtext = parameter as string;
            if (searchboxtext == null) return;
            ArtistModel artistModel = new ArtistModel();
            artistModel.Name = searchboxtext;
            _eventAggregator.GetEvent<ArtistSelectedEvent>().Publish(artistModel.Clone() as ArtistModel);
            _navigationService.NavigateToView<ArtistView>();
        }
    }
}
