using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemon 
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemonById(int id);
        Pokemon GetPokemonByName(string name);
        decimal GetPokemonRating(int pokemonId);
        bool PokemonExist(int pokemonId);
    }
}
