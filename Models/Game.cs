using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace InteractiveTyingGameBlazor.Models
{
    public class Game
    {
        public int CPM { get; set; } = 0;
        public float Accuracy { get; set; } = 0;
        public bool Ready { get; set; } = false;
        public bool Finished { get; set; } = false; 
        public int Placement { get; set; } = 0;
    }
}