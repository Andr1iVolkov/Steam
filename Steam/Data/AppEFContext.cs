using Microsoft.EntityFrameworkCore;
using Steam.Data.Entities;

namespace Steam.Data
{
    public class AppEFContext : DbContext
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            : base(options)
        { }

        public DbSet<GameEntity> Games { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<GameCategoryEntity> GameCategory { get; set; }
        public DbSet<GameImageEntity> GameImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameCategoryEntity>()
                .HasKey(gc => new { gc.GameId, gc.CategoryId });

            modelBuilder.Entity<GameCategoryEntity>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameCategories)
                .HasForeignKey(gc => gc.GameId);

            modelBuilder.Entity<GameCategoryEntity>()
                .HasOne(gc => gc.Category)
                .WithMany(c => c.GameCategories)
                .HasForeignKey(gc => gc.CategoryId);
        }

    }
}
