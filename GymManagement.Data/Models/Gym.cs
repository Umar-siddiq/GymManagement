using System.ComponentModel.DataAnnotations;

namespace GymManagement.Data.Models
{
    public class Gym
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public int? DumbBells { get; set; }
        [Required]
        public int? Treadmills { get; set; }



	}
}
   