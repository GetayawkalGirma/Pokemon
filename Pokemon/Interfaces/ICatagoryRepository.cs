using Pokemon.Models;

namespace PokemonReviewer.Interfaces
{
    public interface ICatagoryRepository
    {
        ICollection<Catagory> GetCatagories();
        Catagory GetCatagory(int id);
        ICollection<Pokemonn> GetPokemonByCatagoryId(int catagoryId);
        bool CatagoryExists(int id);
        bool CreateCatagory(Catagory catagory);
        bool Save();

    }
}
