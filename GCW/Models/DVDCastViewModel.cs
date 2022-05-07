namespace GCW.Models
{
    public class DVDCastViewModel
    {
        public DVDTitle dvdTitle { get; set; }
        public Producer producer { get; set; }
        public Studio studio { get; set; }
        public CastMember castMember { get; set; }
        public Actor actor { get; set; }

    }
}
