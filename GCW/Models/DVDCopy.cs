using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCW.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyNumber { get; set; }

        [ForeignKey("DVDTitle")]
        public int DVDNumber { get; set; }
        public DVDTitle? DVDTitle { get; set; }

        public DateTime DatePurchased { get; set; }

    }
}
