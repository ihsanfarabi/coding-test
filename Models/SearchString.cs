using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iaccess_test.Models
{
    [Table("T_SearchString")]
    public class SearchString
    {
        [Key]
        [Column("String_ID")]
        public Guid StringId { get; set; }

        [Column("String_Content", TypeName = "nvarchar(MAX)")]
        [Display(Name = "String Content")]
        public string StringContent { get; set; }
    }

    public class SearchStringDTO : SearchString
    {
        [Display(Name = "String ID")]
        public new string StringId { get; set; }

        [Display(Name = "Match Times")]
        public string MatchTimes { get; set; }
    }
}
