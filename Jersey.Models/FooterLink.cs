using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jersey.Models
{
    public class FooterLink
    {
        public int Id { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public string LinkText { get; set; } = string.Empty;
        public string LinkUrl { get; set; } = string.Empty;
    }
}
