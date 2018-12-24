namespace WebOdevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int CommentId { get; set; }

        [StringLength(250)]
        public string CommentContent { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CommentDate { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
