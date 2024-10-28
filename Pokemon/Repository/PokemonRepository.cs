using Pokemon.Data;
using Pokemon.Models;
using PokemonReviewer.Interfaces;



namespace PokemonReviewer.Repository
{
    public class PokemonRepository : Ipokemonrepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public Pokemonn GetPokemon(int id)
        {
            return  _context.Pokemons.Where(p=>p.Id == id).FirstOrDefault(); 
        }

        public Pokemonn GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemonn> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        } 

        public bool PokemonExists(int pokeid)
        {
            return _context.Pokemons.Any(p=>p.Id==pokeid);
        }

        public bool CreatePokemon(int ownerId, int catagoryId, Pokemonn pokemon)
        {
            var PokemonOwnerEntity=_context.Owners.Where(a=>a.Id==ownerId).FirstOrDefault();
            var category=_context.Catagories.Where(cat=>cat.Id==catagoryId).FirstOrDefault();
            var pokemonOwner = new PokemonOwner()
            {
                Owner = PokemonOwnerEntity,
                Pokemon = pokemon,
            };
            _context.Add(pokemonOwner);
            var pokemonCatagory = new PokemonCatagory()
            {
                Catagory = category,
                Pokemon = pokemon,
            };
            _context.Add(pokemonCatagory);

            _context.Add(pokemon);
            return Save();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0? true: false;
        }
    }
}
