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
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.Cat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comment)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comment)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
