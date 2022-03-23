using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> Walks { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public string TotalTime
        {
            get
            {
                int time = 0;
                foreach (var walk in Walks)
                {
                    time += walk.Duration;
                }
                int hours = time / 3600;
                int minutes = (time / 60) % 60;
                string totalTimeString = "";
                if (hours > 0) totalTimeString += $"{hours} hrs";
                if (hours > 0 && minutes > 0) totalTimeString += "  and";
                if (minutes > 0) totalTimeString += $" {minutes} minutes";
                return totalTimeString;
            }
        }
    }
}
