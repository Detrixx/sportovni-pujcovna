using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Uzivatel> Uzivatele { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Vybaveni> Vybaveni { get; set; }
    public DbSet<TypVybaveni> TypyVybaveni { get; set; }
    public DbSet<StavVybaveni> StavyVybaveni { get; set; }

    public DbSet<Rezervace> Rezervace { get; set; }
    public DbSet<RezervacePolozka> RezervacePolozky { get; set; }
    public DbSet<StavRezervace> StavyRezervace { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Uzivatel>().ToTable("UZIVATEL");
        modelBuilder.Entity<Role>().ToTable("ROLE");
        modelBuilder.Entity<Vybaveni>().ToTable("VYBAVENI");
        modelBuilder.Entity<TypVybaveni>().ToTable("TYP_VYBAVENI");
        modelBuilder.Entity<StavVybaveni>().ToTable("STAV_VYBAVENI");

        modelBuilder.Entity<Rezervace>().ToTable("REZERVACE");
        modelBuilder.Entity<StavRezervace>().ToTable("STAV_REZERVACE");
        modelBuilder.Entity<RezervacePolozka>().ToTable("REZERVACE_POLOZKA")
            .HasKey(r => new { r.IdRezervace, r.IdVybaveni });


        modelBuilder.Entity<Uzivatel>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.IdRole);

        modelBuilder.Entity<Vybaveni>()
            .HasOne(v => v.TypVybaveni)
            .WithMany()
            .HasForeignKey(v => v.IdTyp);

        modelBuilder.Entity<Vybaveni>()
            .HasOne(v => v.StavVybaveni)
            .WithMany()
            .HasForeignKey(v => v.IdStav);
    }
}