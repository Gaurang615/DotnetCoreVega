using System.ComponentModel.DataAnnotations;

namespace vega.Core.Models
{
    public class Feature
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

    }
}