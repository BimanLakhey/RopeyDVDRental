using System.ComponentModel.DataAnnotations;

namespace GCW.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }
        public string? LoanTypeName { get; set; }
        public int LoanDuration { get; set; }
    }
}
