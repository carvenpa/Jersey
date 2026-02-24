using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Jersey.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("League Name or International Team Name")]
        public string CatName { get; set; }

        [Range(1,50)]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
