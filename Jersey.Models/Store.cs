using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Jersey.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
    }
}
