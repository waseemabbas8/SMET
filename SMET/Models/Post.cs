using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMET.Models
{
    public partial class Post
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(200)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [StringLength(500)]
        public string FeaturedImage { get; set; }
        [StringLength(500)]
        public string Docs { get; set; }
        public int? CategoryId { get; set; }
        public string Extras { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}
