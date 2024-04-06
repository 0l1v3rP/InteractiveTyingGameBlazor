using YoutubeExplode;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos;

namespace InteractiveTyingGameBlazor.Utils
{
    public static class YTMetadataExtract
    {
        private static readonly YoutubeClient _client = new();

        public static async Task<ClosedCaptionTrack?> GetSubtitles(string videoUrl, string language)
        => (await GetManifest(videoUrl)).GetByLanguage(language) is ClosedCaptionTrackInfo trackInfo
            ? await _client.Videos.ClosedCaptions.GetAsync(trackInfo) : null;
        
            
        public static async Task<IEnumerable<Language>> GetTranscripts(string videoUrl)      
            => (await GetManifest(videoUrl)).Tracks.Select(i => i.Language);
        

        private static async Task<ClosedCaptionManifest> GetManifest(string videoUrl)
            => await _client.Videos.ClosedCaptions.GetManifestAsync(videoUrl);
    }
}