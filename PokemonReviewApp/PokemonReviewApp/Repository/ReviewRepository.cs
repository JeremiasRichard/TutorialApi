using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReview
    {
        private readonly ApplicationDbContext _dbContext;

        public ReviewRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateReview(Review review)
        {
            _dbContext.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _dbContext.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _dbContext.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int revienwId)
        {
            return _dbContext.Reviews.Where(x => x.Id == revienwId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _dbContext.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokemonId)
        {
            return _dbContext.Reviews.Where(x => x.Pokemon.Id == pokemonId).ToList();
        }

        public bool ReviewExist(int revienwId)
        {
            return _dbContext.Reviews.Any(x => x.Id == revienwId);

        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _dbContext.Update(review);
            return Save();
        }
    }
}
