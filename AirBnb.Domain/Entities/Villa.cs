using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnb.Domain.Entities
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price per night")]
        public double Price { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name="Image")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity> VillaAmenity { get; set; }
        [NotMapped]
        public bool IsAvailable { get; set; } = true;
    }
}
