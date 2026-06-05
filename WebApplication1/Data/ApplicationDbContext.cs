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

    public DbSet<Meal> Meals => Set<Meal>();

    public DbSet<Diet> Diets => Set<Diet>();

    public DbSet<DietPlanDay> DietPlanDays => Set<DietPlanDay>();

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    public DbSet<ProgressEntry> ProgressEntries => Set<ProgressEntry>();

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

        builder.Entity<Meal>(entity =>
        {
            entity.ToTable("Meals");

            entity.Property(meal => meal.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(meal => meal.Description)
                .HasMaxLength(500);
        });

        builder.Entity<Diet>(entity =>
        {
            entity.ToTable("Diets");
            entity.Property(d => d.Name).HasMaxLength(150).IsRequired();
        });

        builder.Entity<DietPlanDay>(entity =>
        {
            entity.ToTable("DietPlanDays");

            entity.HasOne(dp => dp.Diet)
                .WithMany(d => d.PlanDays)
                .HasForeignKey(dp => dp.DietId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(dp => dp.Meal)
                .WithMany(m => m.PlanDays)
                .HasForeignKey(dp => dp.MealId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<UserProfile>(entity =>
        {
            entity.HasIndex(profile => profile.UserId)
                .IsUnique();

            entity.Property(profile => profile.HeightCm)
                .HasPrecision(5, 2);

            entity.Property(profile => profile.WeightKg)
                .HasPrecision(5, 2);

            entity.HasOne(profile => profile.User)
                .WithOne()
                .HasForeignKey<UserProfile>(profile => profile.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<TrainingSession>(entity =>
        {
            entity.ToTable("UserTrainingSessions");

            entity.HasIndex(session => new { session.UserId, session.SessionDate });

            entity.HasOne(session => session.User)
                .WithMany()
                .HasForeignKey(session => session.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ProgressEntry>(entity =>
        {
            entity.HasIndex(progress => new { progress.UserId, progress.EntryDate });

            entity.Property(progress => progress.WeightKg)
                .HasPrecision(5, 2);

            entity.Property(progress => progress.ChestCm)
                .HasPrecision(5, 2);

            entity.Property(progress => progress.WaistCm)
                .HasPrecision(5, 2);

            entity.Property(progress => progress.HipCm)
                .HasPrecision(5, 2);

            entity.HasOne(progress => progress.User)
                .WithMany()
                .HasForeignKey(progress => progress.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
