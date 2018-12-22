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
        public int PostId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Context { get; set; }

        [StringLength(250)]
        public string Photo { get; set; }

        public DateTime? Time { get; set; }

        public int? CatId { get; set; }

        public int? UserId { get; set; }

        public int? Reads { get; set; }

        public virtual Cat Cat { get; set; }

        public virtual User User { get; set; }
    }
}
