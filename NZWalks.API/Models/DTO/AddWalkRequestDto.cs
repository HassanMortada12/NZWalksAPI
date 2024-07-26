using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description length can't be more than 500 characters.")]
        public string Description { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Length must be greater than 0 km.")]
        public double LengthInKm { get; set; }

        [Required(ErrorMessage = "DifficultyId is required")]
        public int DifficultyId { get; set; }

        [Required(ErrorMessage = "RegionId is required")]
        public int RegionId { get; set; }

        // Navigation properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
