using Pokemon.Models;

namespace PokemonReviewer.Interfaces
{
    public interface IcountryRepository
    {
        ICollection<Country>GetCountries();
        Country GetCountry(int countryId);

        Country GetCountryByOwner(int OwnerId);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);

        // Post
        bool CreateCountry(Country country);
        bool Save();
    }
}

