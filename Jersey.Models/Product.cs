using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Jersey.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }
        [Required]
        public string? Size { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price { get; set; }

        public string? Season { get; set; }

        [Display(Name="Type of Sleeve")]
        public char SleeveType { get; set; }

        [Display(Name="Remarks")]
        public string? Description { get; set; }

        [Display(Name="Ordinary Jersey (Fans) OR Authentic Jersey (Player)")]
        public string? Edition { get; set; }

        //Use EF to link the table Category
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]        
        public string? ImageUrl { get; set; }

        [ValidateNever]
        [Display(Name = "Printing (Color)")]
        public string? Printing { get; set; }
    }
}
