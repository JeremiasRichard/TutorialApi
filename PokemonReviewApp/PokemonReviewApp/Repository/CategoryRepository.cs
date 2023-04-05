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

        public bool CreateCategory(Category category)
        {
             _dbContext.Add(category);
             return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _dbContext.Remove(category);
            return Save();
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

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();    
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
           _dbContext.Update(category);
            return Save();
        }
    }
}
