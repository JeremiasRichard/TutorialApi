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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _dbContext.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _dbContext.Categories.Where(x => x.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,

            };
            _dbContext.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            _dbContext.Add(pokemonCategory);
            _dbContext.Add(pokemon);
            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _dbContext.Remove(pokemon);
            return Save();
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

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _dbContext.Update(pokemon);
            return Save();
        }
    }
}
