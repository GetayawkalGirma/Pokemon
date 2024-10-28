using AutoMapper;
using Pokemon.Data;
using Pokemon.Models;
using PokemonReviewer.Interfaces;

namespace PokemonReviewer.Repository
{
    public class OwnerRepository:IOwnerRepository
    {
        private readonly DataContext _context;
   

        public OwnerRepository(DataContext context )
        {
            _context = context;
           
        }

        public Owner GetOwner(int ownerid)
        {
            return _context.Owners.Where(o => o.Id == ownerid).FirstOrDefault();
             
        }

        public ICollection<Owner> GetOwnerByPokemon(int pokeid)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeid).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemonn> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(p => p.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        Pokemonn IOwnerRepository.Getpokemonbyownername(string ownername)
        {
            var pokeowner = _context.PokemonOwners.FirstOrDefault(p => p.Owner.FirstName == ownername);
            var poke = _context.Pokemons.FirstOrDefault(p => p.Id == pokeowner.PokemonId);
            return poke;
        }

        bool IOwnerRepository.OwnerExists(string ownername)
        {
            return _context.Owners.Any(own => own.FirstName == ownername);
        }

        public bool Save()
        {
            return _context.SaveChanges()> 0? true: false;
        }
    }
}
