using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCW.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }
        public string Title { get; set; }
        public DateTime DateReleased { get; set; }
        public int StandardCharge { get; set; }
        public int PenaltyCharge { get; set; }


        [ForeignKey("DVDCategory")]
        public int CategoryNumber { get; set; }
        
        [ForeignKey("Studio")]
        public int StudioNumber { get; set; }
        
        [ForeignKey("Producer")]
        public int ProducerNumber { get; set; }

        public Studio? Studio { get; set; }
        public Producer? Producer { get; set; }
        public DVDCategory? DVDCategory { get; set; }

    }
}
