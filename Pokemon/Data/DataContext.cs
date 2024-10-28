using Microsoft.EntityFrameworkCore;
using Pokemon.Models;

namespace Pokemon.Data
{
    public class DataContext:DbContext

    {
        public DataContext(DbContextOptions<DataContext>options): base(options)
        {
            
        }

        public DbSet<Catagory>Catagories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemonn> Pokemons { get; set; }
        public DbSet<PokemonOwner> PokemonOwners{ get; set; }
        public DbSet<PokemonCatagory> pokemonCatagories { get; set; }   
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCatagory>()
                .HasKey(pc => new {pc.PokemonId,pc.CatagoryId});

            modelBuilder.Entity<PokemonCatagory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCatagories)
                .HasForeignKey(c => c.CatagoryId);

            modelBuilder.Entity<PokemonOwner>()
                .HasKey(po => new { po.PokemonId, po.OwnerId });

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(c => c.PokemonId);

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(c => c.OwnerId);
        }
    }
}