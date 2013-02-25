using GrigCorePlayer.Model;
using Microsoft.Practices.Prism.Events;

namespace GrigCorePlayer.EventAggregators
{
    public class ArtistSelectedEvent : CompositePresentationEvent<ArtistModel>
    {
    }

    public class PlayTrackEvent : CompositePresentationEvent<TrackModel>
    {
    }

    public class PlayerCommandEvent : CompositePresentationEvent<MediaModel>
    {
    }

    public class NowPlayingUpdateEvent : CompositePresentationEvent<NowPlayingModel>
    {
    }

    public class UpdateMetaInfoEvent : CompositePresentationEvent<MediaMetaDataModel>
    {
    }

    public class StationUpdateEvent : CompositePresentationEvent<StationModel>
    {
    }

    public class UserUpdateEvent : CompositePresentationEvent<UserModel>
    {
    }

    public class LoginChangeEvent : CompositePresentationEvent<LoginModel>
    {
    }
}
