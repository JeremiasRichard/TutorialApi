using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategory
    {
        ICollection<Category> GetCategories();
        public Category GetCategory(int Id);
        ICollection<Pokemon> GetPokemonByCategoryId(int categoryId);
        bool CategoryExist(int categoryId);
      

    }
}
