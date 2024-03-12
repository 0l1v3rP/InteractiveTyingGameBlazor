using YoutubeExplode.Videos.ClosedCaptions;

namespace InteractiveTyingGameBlazor.Models
{
    public class GameConfig(string url, int time, ClosedCaptionTrack? captions, string title)
    {
        public string URL { get; } = url;
        public int Time { get; } = time;
        public ClosedCaptionTrack? Captions { get; } = captions;
        public string Title { get; } = title;
    }
}