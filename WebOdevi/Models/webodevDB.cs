namespace WebOdevi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class webodevDB : DbContext
    {
        public webodevDB()
            : base("name=webodevDB")
        {
        }

        public virtual DbSet<Cat> Cat { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
