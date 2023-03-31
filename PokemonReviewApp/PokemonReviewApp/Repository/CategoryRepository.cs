using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
             _dbContext = dbContext;
        }
        public bool CategoryExist(int categoryId)
        {
            return _dbContext.Categories.Any(x => x.Id == categoryId);
        }

        public ICollection<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategory(int Id)
        {
            return _dbContext.Categories.Where(x => x.Id == Id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategoryId(int categoryId)
        {
            return _dbContext.PokemonCategories.Where(x=> x.CategoryId == categoryId)
                .Select(c => c.Pokemon).ToList();
        }
    }
}
