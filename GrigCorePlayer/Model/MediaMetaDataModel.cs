namespace GrigCorePlayer.Model
{
    public enum EventType
    {
        MediaOpened,
        MediaPlaying
    }

    public class MediaMetaDataModel
    {
        public EventType MediaEventType { get; set; }

        public double Duration { get; set; }

        public double Position { get; set; }
    }
}
