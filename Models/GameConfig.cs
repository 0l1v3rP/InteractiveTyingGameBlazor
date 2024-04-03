using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;

namespace InteractiveTyingGameBlazor.Models
{
    public class GameConfig(string url, int time, ClosedCaptionTrack? captions, string title, Guid videoId)
    {
        public string URL { get; } = url;
        public Guid VideoId { get; } = videoId;
        public int Time { get; } = time;
        public ClosedCaptionTrack? Captions { get; } = captions;
        public string Title { get; } = title;
    }
}