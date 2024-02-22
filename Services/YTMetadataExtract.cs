using YoutubeExplode;
using YoutubeExplode.Videos.ClosedCaptions;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeExplode.Videos;

namespace InteractiveTyingGameBlazor.Utils
{
    public static class YTMetadataExtract
    {
        private static readonly YoutubeClient _client = new();

        public static async Task<bool> VideoExists(string url)
        {
            try
            {
                var videoId = VideoId.TryParse(url).Value;
                var video = await _client.Videos.GetAsync(videoId);
                return video != null;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<ClosedCaptionTrack?> GetSubtitles(string videoUrl, string language)
        {
            var trackInfo = (await GetManifest(videoUrl)).GetByLanguage(language);
            if (trackInfo != null)
            {
                return await _client.Videos.ClosedCaptions.GetAsync(trackInfo);
            }

            return null;
        }

        public static async Task<IEnumerable<Language>> GetTranscripts(string videoUrl)
        {
            return (await GetManifest(videoUrl)).Tracks.Select(i => i.Language);
        }

        private static async Task<ClosedCaptionManifest> GetManifest(string videoUrl)
        {
            return await _client.Videos.ClosedCaptions.GetManifestAsync(videoUrl);
        }
    }
}