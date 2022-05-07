namespace GCW.Models
{
    public class InactiveMembersViewModel
    {
        public DVDTitle dvdTitle { get; set; }
        public DVDCopy dvdCopy { get; set; }
        public Loan loan { get; set; }
        public Member member { get; set; }
    }
}
