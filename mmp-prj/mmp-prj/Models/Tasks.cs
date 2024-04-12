using System.ComponentModel.DataAnnotations;

namespace mmp_prj.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Duration must be a positive number")]
        public int Duration { get; set; }
    }
}
