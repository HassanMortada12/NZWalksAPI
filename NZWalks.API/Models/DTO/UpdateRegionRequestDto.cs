﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required(ErrorMessage = "Code is required")]
        [StringLength(10, ErrorMessage = "Code length can't be more than 10 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }
    }
}
