using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomListBox;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Interfaces;
using GrigCorePlayer.Model;
using GrigCorePlayer.Services;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Views.Station;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCorePlayer.Commands.Utilities
{
    [UsedImplicitly]
    public class InfoUpdateAction : IActionBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFrameNavigationService _navigationService;

        public InfoUpdateAction(IEventAggregator eventAggregator, IFrameNavigationService navigationService)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
        }

        public void UpdateArtistSupport(TilesListBox tilesListBox)
        {
            tilesListBox.ItemDoubleClicked += (sender, args) =>
                {
                    _eventAggregator.GetEvent<ArtistSelectedEvent>().
                            Publish(new ArtistModel
                            {
                                Name = tilesListBox.ItemsSources[tilesListBox.SelectedIndex].Title
                            });
                    _navigationService.NavigateToView<ArtistView>();
                };

        }

        public void PlayTrackSupport(TrackListBox trackListBox)
        {
            trackListBox.TrackSelected += (sender, args) =>
                {
                    // ReSharper disable ConvertToLambdaExpression
                    _eventAggregator.GetEvent<PlayTrackEvent>().Publish(new TrackModel
                    {
                        TrackIndex = trackListBox.SelectedIndex,
                        TrackList = trackListBox.SourceCollection
                    });
                    // ReSharper restore ConvertToLambdaExpression
                };

        }

        public void TagSelectSupport(TagsListBox tagsListBox)
        {
            tagsListBox.OnTagsListBoxItemMouseUp += (o, args) =>
                {
                    _eventAggregator.GetEvent<StationUpdateEvent>()
                        .Publish(new StationModel { TagName = tagsListBox.SelectedName });
                    _navigationService.NavigateToView<StationView>();
                };
        }
    }
}
