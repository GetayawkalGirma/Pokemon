using Pokemon.Models;

namespace PokemonReviewer.Interfaces
{
    public interface Ipokemonrepository
    {
        ICollection<Pokemonn> GetPokemons();
        Pokemonn GetPokemon(int id);
        Pokemonn GetPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeid);
        bool CreatePokemon(int ownerId, int catagoryId, Pokemonn pokemon);
        bool Save();

    }
}
