using System.ComponentModel.DataAnnotations;

namespace GCW.Models
{
    public class DVDCategory
    {
        [Key]
        public int CategoryNumber { get; set; }
        public string? CategoryDescription { get; set; }
        public string? AgeRestriction { get; set; }

    }
}
