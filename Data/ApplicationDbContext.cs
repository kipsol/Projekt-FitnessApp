using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PlanTreningowy> PlanyTreningowe => Set<PlanTreningowy>();

    public DbSet<Cwiczenie> Cwiczenia => Set<Cwiczenie>();

    public DbSet<PartiaMiesniowa> PartieMiesniowe => Set<PartiaMiesniowa>();

    public DbSet<Maszyna> Maszyny => Set<Maszyna>();

    public DbSet<Sekcja> Sekcje => Set<Sekcja>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PlanTreningowy>(entity =>
        {
            entity.ToTable("PlanyTreningowe");

            entity.Property(plan => plan.Nazwa)
                .HasMaxLength(120)
                .IsRequired();

            entity.Property(plan => plan.Opis)
                .HasMaxLength(1000);

            entity.Property(plan => plan.PoziomZaawansowania)
                .HasMaxLength(40)
                .IsRequired();
        });

        builder.Entity<Cwiczenie>(entity =>
        {
            entity.ToTable("Cwiczenia");

            entity.Property(cwiczenie => cwiczenie.Nazwa)
                .HasMaxLength(120)
                .IsRequired();

            entity.Property(cwiczenie => cwiczenie.OpisWykonania)
                .HasMaxLength(2000)
                .IsRequired();

            entity.Property(cwiczenie => cwiczenie.LiczbaPowtorzen)
                .HasMaxLength(30)
                .IsRequired();

            entity.HasOne(cwiczenie => cwiczenie.PartiaMiesniowa)
                .WithMany(partia => partia.Cwiczenia)
                .HasForeignKey(cwiczenie => cwiczenie.PartiaMiesniowaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cwiczenie => cwiczenie.PlanTreningowy)
                .WithMany(plan => plan.Cwiczenia)
                .HasForeignKey(cwiczenie => cwiczenie.PlanTreningowyId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(cwiczenie => cwiczenie.Maszyna)
                .WithMany(maszyna => maszyna.Cwiczenia)
                .HasForeignKey(cwiczenie => cwiczenie.MaszynaId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<PartiaMiesniowa>(entity =>
        {
            entity.ToTable("PartieMiesniowe");

            entity.Property(partia => partia.Nazwa)
                .HasMaxLength(120)
                .IsRequired();
        });

        builder.Entity<Maszyna>(entity =>
        {
            entity.ToTable("Maszyny");

            entity.Property(maszyna => maszyna.Nazwa)
                .HasMaxLength(120)
                .IsRequired();

            entity.Property(maszyna => maszyna.Status)
                .HasMaxLength(40)
                .IsRequired();

            entity.HasOne(maszyna => maszyna.Sekcja)
                .WithMany(sekcja => sekcja.Maszyny)
                .HasForeignKey(maszyna => maszyna.SekcjaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Sekcja>(entity =>
        {
            entity.ToTable("Sekcje");

            entity.Property(sekcja => sekcja.Nazwa)
                .HasMaxLength(120)
                .IsRequired();
        });
    }
}
