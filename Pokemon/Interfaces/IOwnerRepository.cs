using Microsoft.AspNetCore.Connections;
using Pokemon.Models;

namespace PokemonReviewer.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
        Pokemonn Getpokemonbyownername(string ownername);
        ICollection<Owner> GetOwnerByPokemon(int pokeid);
        ICollection<Pokemonn> GetPokemonByOwner(int ownerId);

        bool OwnerExists(int ownerId);
        bool OwnerExists(string ownername);

        bool CreateOwner(Owner owner);
        bool Save();


    }
}
