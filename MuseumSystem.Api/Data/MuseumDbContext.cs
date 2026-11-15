using Microsoft.EntityFrameworkCore;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Api.Data;

public class MuseumDbContext(DbContextOptions<MuseumDbContext> options) : DbContext(options)
{
    public DbSet<Exhibition> Exhibitions => Set<Exhibition>();
    public DbSet<Excursion> Excursions => Set<Excursion>();
    public DbSet<Visitor> Visitors => Set<Visitor>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка Exhibition
        modelBuilder.Entity<Exhibition>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            // Theme — enum, HallNumber — int, StartDate/EndDate — DateOnly
            entity.Property(e => e.Theme).IsRequired();
            entity.Property(e => e.HallNumber).IsRequired();
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
        });

        // Настройка Excursion
        modelBuilder.Entity<Excursion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartsAt).IsRequired();
            entity.Property(e => e.DurationMinutes).IsRequired();

            // Связь многие ко многим с Exhibition
            entity.HasMany(e => e.Exhibitions)
                  .WithMany()
                  .UsingEntity(j => j.ToTable("excursion_exhibitions"));
        });

        // Настройка Visitor
        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.BirthDate).IsRequired();
        });

        // Настройка Ticket
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Price).HasPrecision(18, 2);

            // Навигационные свойства
            entity.HasOne(e => e.Excursion)
                  .WithMany()
                  .HasForeignKey("ExcursionId")
                  .IsRequired();

            entity.HasOne(e => e.Visitor)
                  .WithMany()
                  .HasForeignKey("VisitorId")
                  .IsRequired();
        });
    }
}