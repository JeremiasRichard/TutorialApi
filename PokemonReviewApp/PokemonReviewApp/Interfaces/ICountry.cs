using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountry 
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int Id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExist(int Id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);   
        bool DeleteCountry(Country country);
        bool Save();


    }
}
