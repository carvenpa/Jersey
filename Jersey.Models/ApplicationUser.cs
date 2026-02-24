using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;


namespace Jersey.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        public int? StoreId { get; set; } //align with the table Store to associate the new staff or Admin with the store
        [ForeignKey("StoreId")]
        [ValidateNever]
        public Store Store { get; set; }
    }
}
