using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategory
    {
        ICollection<Category> GetCategories();
        public Category GetCategory(int Id);
        ICollection<Pokemon> GetPokemonByCategoryId(int categoryId);
        bool CategoryExist(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
      

    }
}
