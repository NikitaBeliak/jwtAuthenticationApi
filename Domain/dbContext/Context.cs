using Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Domain.dbContext
{
    public partial class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.idUsers).HasName("idUsers");
                e.ToTable("User");
                e.Property(x => x.Username).HasColumnName("Username");
                e.Property(x => x.FirstName).HasColumnName("FirstName");
                e.Property(x => x.LastName).HasColumnName("LastName");
                e.Property(x => x.Password).HasColumnName("Password");

            });

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
