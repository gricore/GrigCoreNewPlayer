using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomItems;
using GrigCorePlayer.Model;
using GrigCoreLastfm.API.Types;
using GrigCoreLastfm.Lastfm;

namespace GrigCorePlayer.Services
{
    [UsedImplicitly]
    public class LastFmService : ILastFmService
    {
        private readonly IDataService _dataService;
        private readonly ITextParser _textParser;

        public LastFmService(IDataService dataService, ITextParser textParser)
        {
            _dataService = dataService;
            _textParser = textParser;
        }

        /// <summary>
        /// Get artist model by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArtistModel GetArtisBasicInfoByName(string name)
        {
            var session = _dataService.GetLfmSessionFromSettings();
            var artist = new Artist(name, session);
            artist.GetInfo(false);

            // Clear bio text
            var bioText = artist.Bio.Summary;
            bioText = bioText.Replace(@"<![CDATA[", string.Empty);
            bioText = bioText.Replace(@"]]>", string.Empty);
            bioText = _textParser.StripTagsRegexCompiled(bioText);
            bioText = _textParser.ReplaceAmpersand(bioText);

            return new ArtistModel
                {
                    Name = _textParser.ReplaceAmpersand(artist.Name),
                    ArtistBio = bioText,
                    PictureUrl = artist.Images.Extralarge
                };
        }

        /// <summary>
        /// Get artist top tags by artist name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TagsListBoxItemsCollection GetArtistTopTagsByName(string name)
        {
            var sources = new TagsListBoxItemsCollection();
            var session = _dataService.GetLfmSessionFromSettings();
            var artist = new Artist(name, session);
            artist.GetInfo(false);

            foreach (var tag in artist.Tags)
            {
                sources.Add(new TagsListBoxItem { Title = _textParser.ReplaceAmpersand(tag.Name) });
            }
            return sources;
        }

        /// <summary>
        /// Get albums by artist name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TilesListBoxItemSources GetAlbumsByArtistName(string name)
        {
            var sources = new TilesListBoxItemSources();
            var session = _dataService.GetLfmSessionFromSettings();
            var artist = new Artist(name, session);
            artist.GetTopAlbums(false);

            sources.Add(new TilesListBoxItem { Id = 1, Picture = string.Empty, Title = "Top Tracks" });

            int id = 1;
            foreach (var artist1 in artist.Albums)
            {
                id++;
                sources.Add(new TilesListBoxItem
                {
                    Id = id,
                    Picture = artist1.Images.Large,
                    Title = _textParser.ReplaceAmpersand(artist1.Name)
                });
            }
            return sources;
        }

        /// <summary>
        /// Get similar by artist name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TilesListBoxItemSources GetSimilarByName(string name)
        {
            var sources = new TilesListBoxItemSources();
            var session = _dataService.GetLfmSessionFromSettings();
            var artist = new Artist(name, session);
            artist.GetSimilarArtists(false);

            int id = 0;
            foreach (var artist1 in artist.Similar)
            {
                id++;
                sources.Add(new TilesListBoxItem
                    {
                        Id = id,
                        Picture = artist1.Images.Large,
                        Title = _textParser.ReplaceAmpersand(artist1.Name)
                    });
            }
            return sources;
        }

        /// <summary>
        /// Gets album tracks
        /// </summary>
        /// <param name="artistName"></param>
        /// <param name="albumName"></param>
        /// <returns></returns>
        public TrackListBoxItemCollection GetAlbumTracksByArtistName(string artistName, string albumName)
        {
            var sources = new TrackListBoxItemCollection();
            var session = _dataService.GetLfmSessionFromSettings();
            var artist = new Artist(artistName, session);
            var album = new Album { Artist = artist, Session = session, Name = albumName };
            var tracks = new List<Track>();

            if (albumName == "Top Tracks")
            {
                tracks = artist.GetTopTracks(200);
            }
            else
            {
                album.GetInfo(false);
                tracks = album.Tracks;
            }



            int i = 0;
            foreach (var track in tracks)
            {
                i++;
                sources.Add(new TrackListBoxItem
                {
                    Track = _textParser.ReplaceAmpersand(track.Name),
                    Artist = _textParser.ReplaceAmpersand(track.Artist.Name),
                    Duration = _textParser.FormatToTime(track.Duration),
                    Index = i.ToString(CultureInfo.InvariantCulture)
                });
            }

            return sources;
        }

        /// <summary>
        /// Get tag top track
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TrackListBoxItemCollection GetTagTopTrackByTagName(string name)
        {
            var sources = new TrackListBoxItemCollection();
            var session = _dataService.GetLfmSessionFromSettings();
            var tag = new Tag(name, session);
            tag.GetTopTracks();

            int i = 0;
            foreach (var track in tag.Tracks)
            {
                i++;
                sources.Add(new TrackListBoxItem
                {
                    Track = _textParser.ReplaceAmpersand(track.Name),
                    Artist = _textParser.ReplaceAmpersand(track.Artist.Name),
                    Duration = _textParser.FormatToTime(track.Duration),
                    Index = i.ToString(CultureInfo.InvariantCulture)
                });
            }
            return sources;
        }

        /// <summary>
        /// Get tag top artists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TilesListBoxItemSources GetTagTopArtistsByTagName(string name)
        {
            var sources = new TilesListBoxItemSources();
            var session = _dataService.GetLfmSessionFromSettings();
            var tag = new Tag(name, session);
            tag.GetTopArtists();

            int i = 0;
            foreach (var artist in tag.Artists)
            {
                i++;
                sources.Add(new TilesListBoxItem
                {
                    Id = i,
                    Title = _textParser.ReplaceAmpersand(artist.Name),
                    Picture = artist.Images.Large,
                });
            }
            return sources;
        }

        /// <summary>
        /// Last.fm Login request
        /// </summary>
        /// <returns></returns>
        public HomeModel LfmLoginRequest()
        {
            var session = _dataService.GetLfmSessionFromSettings();
            session.GetSession();
            _dataService.WriteLfmSessionToSettings(session);
            var user = new User(session.Name, session);
            user.GetInfo();

            return new HomeModel
                {
                    LoginText = _textParser.ReplaceAmpersand(user.Realname),
                    LoginPicture = user.Images.Large
                };
        }

        /// <summary>
        /// Gets a last.fm user data
        /// </summary>
        /// <returns></returns>
        public HomeModel GetLfmUserData()
        {
            var session = _dataService.GetLfmSessionFromSettings();
            var user = new User(session.Name, session);
            user.GetInfo();
            return new HomeModel
            {
                LoginText = _textParser.ReplaceAmpersand(user.Realname),
                LoginPicture = user.Images.Large
            };
        }

        /// <summary>
        /// Hast last.fm session
        /// </summary>
        /// <returns></returns>
        public bool HasLfmSession()
        {
            return !string.IsNullOrWhiteSpace(Properties.Settings.Default.lfm_sessionKey);

        }

        /// <summary>
        /// Scrobble track on last.fm
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="track"></param>
        public void ScrobbleTrack(string artist, string track)
        {
            try
            {
                var session = _dataService.GetLfmSessionFromSettings();
                if (HasLfmSession())
                {
                    Scrobbler scrobbler = new Scrobbler();
                    scrobbler.ScrobbleTrack(session, artist, track);
                }

            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Get user top artists
        /// </summary>
        /// <returns></returns>
        public TilesListBoxItemSources GetUserTopArtistsByUsername()
        {
            if (HasLfmSession())
            {
                var sources = new TilesListBoxItemSources();
                var session = _dataService.GetLfmSessionFromSettings();
                var user = new User(session.Name, session);
                user.GetTopArtists(200);

                int i = 0;
                foreach (var artist in user.Artists)
                {
                    i++;
                    sources.Add(new TilesListBoxItem
                    {
                        Id = i,
                        Title = _textParser.ReplaceAmpersand(artist.Name),
                        Picture = artist.Images.Large,
                    });
                }
                return sources;
            }

            return new TilesListBoxItemSources();
        }

        public TrackListBoxItemCollection GetUserTopTracks()
        {
            if (HasLfmSession())
            {
                var sources = new TrackListBoxItemCollection();
                var session = _dataService.GetLfmSessionFromSettings();
                var user = new User(session.Name, session);
                user.GetTopTracks(300);

                int i = 0;
                foreach (var track in user.Tracks)
                {
                    i++;
                    sources.Add(new TrackListBoxItem
                    {
                        Index = i.ToString(CultureInfo.InvariantCulture),
                        Track = _textParser.ReplaceAmpersand(track.Name),
                        Artist = _textParser.ReplaceAmpersand(track.Artist.Name),
                        Duration = _textParser.FormatToTime(track.Duration)
                    });
                }
                return sources;
            }

            return new TrackListBoxItemCollection();
        }

    }
}
