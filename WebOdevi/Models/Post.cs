namespace WebOdevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public int PostId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Context { get; set; }

        [Required]
        [StringLength(250)]
        public string Photo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Time { get; set; }

        public int CatId { get; set; }

        public int UserId { get; set; }

        public int? Reads { get; set; }

        public virtual Cat Cat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }

        public virtual User User { get; set; }
    }
}
