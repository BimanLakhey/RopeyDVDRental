﻿using System.ComponentModel.DataAnnotations;

namespace GCW.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MembershipCategoryNumber { get; set; }
        public string? MembershipCategoryDescription { get; set; }
        public string? MembershipCategoryTotalLoans { get; set; }
        
    }
}
