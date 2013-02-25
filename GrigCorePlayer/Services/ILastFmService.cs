using GrigCorePlayer.Controls.CustomItems;
using GrigCorePlayer.Model;

namespace GrigCorePlayer.Services
{
    public interface ILastFmService
    {
        /// <summary>
        /// Gets a artist model by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ArtistModel GetArtisBasicInfoByName(string name);

        /// <summary>
        /// Gets a artist albums by artist name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TilesListBoxItemSources GetAlbumsByArtistName(string name);

        /// <summary>
        /// Gets artist top tags by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TagsListBoxItemsCollection GetArtistTopTagsByName(string name);

        /// <summary>
        /// Get similars by artist name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TilesListBoxItemSources GetSimilarByName(string name);

        /// <summary>
        /// Gets album tracks
        /// </summary>
        /// <param name="artistName"></param>
        /// <param name="albumName"></param>
        /// <returns></returns>
        TrackListBoxItemCollection GetAlbumTracksByArtistName(string artistName, string albumName);

        /// <summary>
        /// Get Tag top Tracks
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TrackListBoxItemCollection GetTagTopTrackByTagName(string name);

        /// <summary>
        /// Get Tag top artists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TilesListBoxItemSources GetTagTopArtistsByTagName(string name);

        /// <summary>
        /// Last.fm login request
        /// </summary>
        /// <returns></returns>
        HomeModel LfmLoginRequest();

        /// <summary>
        /// Hast last.fm session
        /// </summary>
        /// <returns></returns>
        bool HasLfmSession();

        /// <summary>
        /// Gets a last.fm user data
        /// </summary>
        /// <returns></returns>
        HomeModel GetLfmUserData();

        /// <summary>
        /// Scrobble track on last.fm
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="track"></param>
        void ScrobbleTrack(string artist, string track);

        /// <summary>
        /// Get user top artists
        /// </summary>
        /// <returns></returns>
        TilesListBoxItemSources GetUserTopArtistsByUsername();

        /// <summary>
        /// Gets user top tracks
        /// </summary>
        /// <returns></returns>
        TrackListBoxItemCollection GetUserTopTracks();
    }
}
