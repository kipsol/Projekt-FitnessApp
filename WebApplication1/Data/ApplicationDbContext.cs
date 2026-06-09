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

    public DbSet<PozycjaPlanu> PozycjePlanu => Set<PozycjaPlanu>();

    public DbSet<PartiaMiesniowa> PartieMiesniowe => Set<PartiaMiesniowa>();

    public DbSet<Maszyna> Maszyny => Set<Maszyna>();

    public DbSet<Sekcja> Sekcje => Set<Sekcja>();

    public DbSet<Meal> Meals => Set<Meal>();

    public DbSet<Diet> Diets => Set<Diet>();

    public DbSet<DietPlanDay> DietPlanDays => Set<DietPlanDay>();

    public DbSet<ClassEvent> ClassEvents => Set<ClassEvent>();

    public DbSet<ClassSchedule> ClassSchedules => Set<ClassSchedule>();

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    public DbSet<UserFitnessAssignment> UserFitnessAssignments => Set<UserFitnessAssignment>();

    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    public DbSet<ProgressEntry> ProgressEntries => Set<ProgressEntry>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

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

            entity.HasMany(plan => plan.PozycjePlanu)
                .WithOne(pozycja => pozycja.PlanTreningowy)
                .HasForeignKey(pozycja => pozycja.PlanTreningowyId)
                .OnDelete(DeleteBehavior.Cascade);
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

            entity.Property(cwiczenie => cwiczenie.PlikSciezka)
                .HasMaxLength(260);

            entity.HasOne(cwiczenie => cwiczenie.PartiaMiesniowa)
                .WithMany(partia => partia.Cwiczenia)
                .HasForeignKey(cwiczenie => cwiczenie.PartiaMiesniowaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cwiczenie => cwiczenie.Maszyna)
                .WithMany(maszyna => maszyna.Cwiczenia)
                .HasForeignKey(cwiczenie => cwiczenie.MaszynaId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<PozycjaPlanu>(entity =>
        {
            entity.ToTable("PozycjePlanu");

            entity.Property(pozycja => pozycja.DzienTreningowy)
                .HasMaxLength(40)
                .IsRequired();

            entity.Property(pozycja => pozycja.LiczbaPowtorzen)
                .HasMaxLength(30)
                .IsRequired();

            entity.HasIndex(pozycja => new { pozycja.PlanTreningowyId, pozycja.CwiczenieId });

            entity.HasOne(pozycja => pozycja.PlanTreningowy)
                .WithMany(plan => plan.PozycjePlanu)
                .HasForeignKey(pozycja => pozycja.PlanTreningowyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pozycja => pozycja.Cwiczenie)
                .WithMany(cwiczenie => cwiczenie.PozycjePlanu)
                .HasForeignKey(pozycja => pozycja.CwiczenieId)
                .OnDelete(DeleteBehavior.Cascade);
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

        builder.Entity<UserFitnessAssignment>(entity =>
        {
            entity.ToTable("UserFitnessAssignments");

            entity.HasIndex(assignment => assignment.UserId)
                .IsUnique();

            entity.HasOne(assignment => assignment.User)
                .WithMany()
                .HasForeignKey(assignment => assignment.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(assignment => assignment.AssignedByTrainer)
                .WithMany()
                .HasForeignKey(assignment => assignment.AssignedByTrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(assignment => assignment.PlanTreningowy)
                .WithMany()
                .HasForeignKey(assignment => assignment.PlanTreningowyId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(assignment => assignment.Diet)
                .WithMany()
                .HasForeignKey(assignment => assignment.DietId)
                .OnDelete(DeleteBehavior.SetNull);
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

        builder.Entity<ClassEvent>(entity =>
        {
            entity.ToTable("ClassEvents");

            entity.Property(ce => ce.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(ce => ce.Trainer)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(ce => ce.Description)
                .HasMaxLength(1000);
        });

        builder.Entity<ClassSchedule>(entity =>
        {
            entity.ToTable("ClassSchedules");

            entity.HasIndex(schedule => new { schedule.UserId, schedule.ClassEventId })
                .IsUnique()
                .HasFilter("[UserId] IS NOT NULL");

            entity.HasOne(schedule => schedule.User)
                .WithMany()
                .HasForeignKey(schedule => schedule.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(cs => cs.ClassEvent)
                .WithMany(ce => ce.Schedules)
                .HasForeignKey(cs => cs.ClassEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
        });

        builder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.HasIndex(order => new { order.UserId, order.OrderDate });

            entity.HasOne(order => order.User)
                .WithMany()
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItems");

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
