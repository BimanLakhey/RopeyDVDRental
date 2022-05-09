namespace GCW.Models
{
    public class MemberLoansViewModel
    {
        public int MemberNumber { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberAddress { get; set; }
        public DateTime MemberDateOfBirth { get; set; }
        public DateTime? DateReturned { get; set; }
        public string MembershipCategoryDesc { get; set; }
        public int CategoryTotalLoans { get; set; }
        public int LoanCount { get; set; }
        public string LoanStatus { get; set; }
    }
}
