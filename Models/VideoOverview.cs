using InteractiveTyingGameBlazor.Models;
using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.Models
{
    public class VideoOverview(RegisteredVideo registeredVideo) 
    {
        public int Counter { get; set; } = registeredVideo.Counter;

        public int CPMAverage { get; set; } = 0;

        public string AddedBy { get; set; } = registeredVideo.User.UserName ?? "Anonymous";

        public string VideoName { get; set; } = registeredVideo.Title;

        public Guid VideoId { get; set; } = registeredVideo.Id;
    }
}
