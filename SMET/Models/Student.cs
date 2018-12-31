using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMET.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Column("CNIC")]
        [StringLength(15)]
        public string Cnic { get; set; }
        [Column("DOB", TypeName = "date")]
        public DateTime? Dob { get; set; }
        [StringLength(100)]
        public string FatherName { get; set; }
        public int? RollNo { get; set; }
        public int? Semester { get; set; }
        [Required]
        [StringLength(100)]
        public string Department { get; set; }
        public int? SessionStart { get; set; }
        public int? SessionEnd { get; set; }
        [StringLength(200)]
        public string Image { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(300)]
        public string StreetAddress { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
    }
}
