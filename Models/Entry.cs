using System.ComponentModel.DataAnnotations;

namespace DogTrackerApi.Models
{
    public class Entry
    {
        [Key]
        public DateTime DateTimeId { get; set; }
        public bool HasPooped { get; set; }
        public bool HasPeed { get; set; }
        public string Name { get; set; }
    }
}
