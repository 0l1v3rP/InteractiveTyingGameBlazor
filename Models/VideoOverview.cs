using InteractiveTyingGameBlazor.DbModels;
using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.Models
{
    public class VideoOverview(RegisteredVideo registeredVideo, int cpmAverage) 
    {
        public int Counter { get; set; } = registeredVideo.Counter;

        public int CPMAverage { get; set; } = cpmAverage;

        public string AddedBy { get; set; } = registeredVideo.User.UserName ?? "Anonymous";

        public string VideoName { get; set; } = registeredVideo.Title;

        public string VideoId { get; set; } = registeredVideo.Id;
        
        public int PressedChars { get; set; } = registeredVideo.PressedChars;

    }
}
