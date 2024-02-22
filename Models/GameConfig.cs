using YoutubeExplode.Videos.ClosedCaptions;

namespace InteractiveTyingGameBlazor.Models
{
    public class GameConfig(string url, int time, ClosedCaptionTrack? captions)
    {
        public string URL { get; set; } = url;
        public int Time { get; set; } = time;
        public ClosedCaptionTrack? Captions { get; set; } = captions;
    }
}