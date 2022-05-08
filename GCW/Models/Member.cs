using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCW.Models
{
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }
        public string? MemberLastName { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberAddress { get; set; }
        public DateTime MemberDateOfBirth { get; set; }
        
        [ForeignKey("MembershipCategory")]
        public int MembershipCategoryNumber { get; set; }
        public MembershipCategory? MembershipCategory { get; set; }

    }
}
