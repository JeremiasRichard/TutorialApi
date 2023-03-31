using Microsoft.EntityFrameworkCore.ChangeTracking;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemon
    {
        private readonly ApplicationDbContext _dbContext;
        public PokemonRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Pokemon GetPokemonById(int pokemonId)
        {
            return _dbContext.Pokemon.Where(p => p.Id == pokemonId).FirstOrDefault();
        }

        public Pokemon GetPokemonByName(string name)
        {
            return _dbContext.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokemonId)
        {
            var review = _dbContext.Reviews.Where(p => p.Pokemon.Id== pokemonId);
            
            if(review.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating)/review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _dbContext.Pokemon.OrderBy(p=> p.Id).ToList();
        }

        public bool PokemonExist(int pokemonId)
        {
            return _dbContext.Pokemon.Any(p => p.Id == pokemonId);
        }
    }
}
