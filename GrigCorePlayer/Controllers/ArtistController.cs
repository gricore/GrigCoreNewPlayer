using System.Windows;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomItems;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Interfaces;
using GrigCorePlayer.Model;
using GrigCorePlayer.Services;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Views.Extentions;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using GrigCorePlayer.Commands.Utilities;

namespace GrigCorePlayer.Controllers
{
    [UsedImplicitly]
    public class ArtistController : IControllerBase
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly IAsyncService _asyncService;
        private readonly IUnityContainer _container;
        private readonly ILastFmService _lastFmService;

        #endregion

        #region Commands

        public ICommand AlbumSelectedCommand { get; set; }

        #endregion

        #region Models

        private ArtistFrameworkModel _frameworkModel = new ArtistFrameworkModel();
        /// <summary>
        /// Gets or sets framework model.
        /// </summary>
        public ArtistFrameworkModel FrameworkModel
        {
            get { return _frameworkModel; }
            set { _frameworkModel = value; }
        }


        private ArtistModel _model = new ArtistModel();
        /// <summary>
        /// Gets or sets artist model
        /// </summary>
        public ArtistModel Model
        {
            get { return _model; }
            set { _model = value; }
        }


        #endregion

        #region Ctor

        public ArtistController(IEventAggregator eventAggregator, IAsyncService asyncService,
            IUnityContainer container, ILastFmService lastFmService)
        {
            _eventAggregator = eventAggregator;
            _asyncService = asyncService;
            _container = container;
            _lastFmService = lastFmService;
            OnCtorLoaded();
        }

        #endregion

        #region Methods

        private void OnCtorLoaded()
        {
            _eventAggregator.GetEvent<ArtistSelectedEvent>().Subscribe(OnArtistSelected);
        }

        private void OnArtistSelected(ArtistModel obj)
        {
            FrameworkModel.ArtistBasicInfoContent = new FrameworkElement();
            FrameworkModel.ArtistAlbumsContent = new FrameworkElement();
            FrameworkModel.TracksContent = new FrameworkElement();

            AlbumSelectedCommand = new DelegateCommand(AlbumSelectedAction);


            ArtistBasicInfoAction(obj);
        }

        #endregion

        #region Utilities

        private void ArtistBasicInfoAction(ArtistModel obj)
        {
            _asyncService.RunAsync(() => { FrameworkModel.ArtistBasicInfoContent = new LoadingView(); },
                () =>
                {
                    try { obj = _lastFmService.GetArtisBasicInfoByName(obj.Name); }
                    catch
                    {
                        obj = new ArtistModel { Name = "An error occured" };
                    }
                },
                                   () =>
                                   {
                                       obj = PrepareArtistModel(obj);

                                       var artistBasicInfoView = _container.Resolve<_BasicInfoView>();
                                       artistBasicInfoView.DataContext = this;
                                       FrameworkModel.ArtistBasicInfoContent = artistBasicInfoView;

                                       ArtistTagsAction(Model);
                                   });
        }

        private void ArtistTagsAction(ArtistModel model)
        {
            Model.Tags = new TagsListBoxItemsCollection();
            var tags = new TagsListBoxItemsCollection();

            _asyncService.RunAsync(() => { FrameworkModel.TagsContent = new LoadingView(); },
               () =>
               {
                   try
                   {
                       tags = _lastFmService.GetArtistTopTagsByName(model.Name).Clone() as TagsListBoxItemsCollection;
                   }
                   catch { }
               }, () =>
               {
                   var artisttags = _container.Resolve<_TagsView>();
                   artisttags.DataContext = this;
                   _container.Resolve<InfoUpdateAction>().TagSelectSupport(artisttags.TopTagsListBox);
                   FrameworkModel.TagsContent = artisttags;

                   // TODO: Here write next action
                   ArtistAlbumsAction(model);

                   if (tags.Count == 0)
                   {
                       FrameworkModel.ArtistAlbumsContent = new FrameworkElement();
                       return;
                   }

                   if (tags.Count > 0)
                       _asyncService.RunAsyncListAdding(tags.Count,
                           index =>
                               Model.Tags.Add(tags[index]), 15);
               });
        }

        private void ArtistAlbumsAction(ArtistModel model)
        {
            Model.Albums = new TilesListBoxItemSources();
            var albums = new TilesListBoxItemSources();

            _asyncService.RunAsync(() => { FrameworkModel.ArtistAlbumsContent = new LoadingView(); },
                () =>
                {
                    try
                    {
                        albums = _lastFmService.GetAlbumsByArtistName(model.Name).Clone() as TilesListBoxItemSources;
                    }
                    catch { }
                }, () =>
                    {
                        var artistalbums = _container.Resolve<_AlbumsView>();
                        artistalbums.DataContext = this;
                        FrameworkModel.ArtistAlbumsContent = artistalbums;

                        ArtistSimilarAction(model);
                        if (albums.Count == 0)
                        {
                            FrameworkModel.ArtistAlbumsContent = new FrameworkElement();
                            return;
                        }

                        model.Albums = albums;
                        //Model.Albums.Add(new TilesListBoxItem { Title = "Top Tracks" });
                        //if (albums.Count > 0)
                        //    _asyncService.RunAsyncListAdding(albums.Count,
                        //        index =>
                        //            Model.Albums.Add(albums[index]), 15);
                    });
        }

        private void ArtistSimilarAction(ArtistModel model)
        {
            //Model.Similar.Clear();
            var similar = new TilesListBoxItemSources();

            _asyncService.RunAsync(() => { FrameworkModel.SimilarContent = new LoadingView(); },
                () =>
                {
                    try
                    {
                        similar = _lastFmService.GetSimilarByName(model.Name).Clone() as TilesListBoxItemSources;
                    }
                    catch { }
                }, () =>
                {
                    var artistsimilar = _container.Resolve<_SimilarView>();
                    artistsimilar.DataContext = this;
                    _container.Resolve<InfoUpdateAction>().UpdateArtistSupport(artistsimilar.SimilarListBox);
                    FrameworkModel.SimilarContent = artistsimilar;

                    if (similar.Count == 0)
                    {
                        FrameworkModel.SimilarContent = new FrameworkElement();
                        return;
                    }

                    Model.Similar = similar;
                    //if (similar.Count > 0)
                    //    _asyncService.RunAsyncListAdding(similar.Count,
                    //        index => Model.Similar.Add(similar[index]), 15);
                });
        }


        private void AlbumSelectedAction()
        {
            Model.Tracks.Clear();
            var tracks = new TrackListBoxItemCollection();

            _asyncService.RunAsync(() => { FrameworkModel.TracksContent = new LoadingView(); }, () =>
            {
                try
                {
                    tracks = _lastFmService.GetAlbumTracksByArtistName(Model.Name,
                                                                         Model.Albums[Model.SelectedAlbumIndex]
                                                                             .Title);
                }
                catch { }

            }, () =>
            {
                var tracksContent = _container.Resolve<_TracksView>();
                tracksContent.DataContext = this;
                // Add play track support. when track selected, play it.
                _container.Resolve<InfoUpdateAction>().PlayTrackSupport(tracksContent.TracksListBox);
                FrameworkModel.TracksContent = tracksContent;

                if (tracks.Count == 0)
                {
                    FrameworkModel.TracksContent = new FrameworkElement();
                    return;
                }

                Model.Tracks = tracks;
                //if (tracks.Count > 0)
                //    _asyncService.RunAsyncListAdding(tracks.Count,
                //        index => Model.Tracks.Add(tracks[index]), 25);
            });
        }

        private ArtistModel PrepareArtistModel(ArtistModel model)
        {
            //if (!string.IsNullOrWhiteSpace(model.Name))
            Model.Name = model.Name;
            //if (!string.IsNullOrWhiteSpace(model.ArtistBio))
            Model.ArtistBio = model.ArtistBio;
            //if (!string.IsNullOrWhiteSpace(model.PictureUrl))
            Model.PictureUrl = model.PictureUrl;

            return Model;
        }

        #endregion
    }
}
