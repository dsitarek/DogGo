using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walk
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int WalkerId { get; set; }
        public int DogId { get; set; }
        [Display(Name = "Client Name")]
        public string OwnerName { get; set; }

        
    }
}
