using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReview
    {
        ICollection<Review> GetReviews();
        Review GetReview(int revienwId);
        ICollection<Review>GetReviewsOfAPokemon(int pokemonId);
        bool ReviewExist(int revienwId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();



    }
}
