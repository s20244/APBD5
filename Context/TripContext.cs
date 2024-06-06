using Microsoft.EntityFrameworkCore;
using TripAPI.Models;

public class TripContext : DbContext
{
    public TripContext(DbContextOptions<TripContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<ClientTrip> ClientTrips { get; set; }
    public DbSet<CountryTrip> CountryTrips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("Client", "trip").HasKey(c => c.IdClient);
        modelBuilder.Entity<Trip>().ToTable("Trip", "trip").HasKey(t => t.IdTrip);
        modelBuilder.Entity<Country>().ToTable("Country", "trip").HasKey(c => c.IdCountry);
        modelBuilder.Entity<ClientTrip>().ToTable("Client_Trip", "trip").HasKey(ct => new { ct.IdClient, ct.IdTrip });
        modelBuilder.Entity<CountryTrip>().ToTable("Country_Trip", "trip").HasKey(ct => new { ct.IdCountry, ct.IdTrip });

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Client)
            .WithMany(c => c.ClientTrips)
            .HasForeignKey(ct => ct.IdClient);

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Trip)
            .WithMany(t => t.ClientTrips)
            .HasForeignKey(ct => ct.IdTrip);

        modelBuilder.Entity<CountryTrip>()
            .HasOne(ct => ct.Country)
            .WithMany(c => c.CountryTrips)
            .HasForeignKey(ct => ct.IdCountry);

        modelBuilder.Entity<CountryTrip>()
            .HasOne(ct => ct.Trip)
            .WithMany(t => t.CountryTrips)
            .HasForeignKey(ct => ct.IdTrip);
    }
}
