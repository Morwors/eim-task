using eim_task.Server.Models;
using Microsoft.EntityFrameworkCore;


namespace eim_task.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JSTask> JSTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JSTask>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(t => t.Description)
                      .HasMaxLength(500);
                entity.Property(t => t.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
