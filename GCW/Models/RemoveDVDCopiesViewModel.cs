namespace GCW.Models
{
    public class RemoveDVDCopiesViewModel
    {
        public DVDTitle dvdTitle { get; set; }
        public DVDCopy dvdCopy { get; set; }
        public Loan loan { get; set; }
    }
}
