using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;   
        public ReviewerRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Reviewer GetReviewer(int reviewerId)
        {
            return _dbContext.Reviewers.Where(x => x.Id == reviewerId)
                 .Include(e => e.Reviews).FirstOrDefault();
        }
        public ICollection<Reviewer> GetReviewers()
        {
            return _dbContext.Reviewers.ToList();
        }
        public ICollection<Review> GetAllReviewsByReviewer(int reviewerId)
        {
            return _dbContext.Reviews.Where(x => x.Reviewer.Id == reviewerId).ToList();
        }
        public bool ReviewerExist(int reviewerId)
        {
            return _dbContext.Reviewers.Any(x => x.Id == reviewerId);
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _dbContext.Add(reviewer);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _dbContext.Update(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _dbContext.Remove(reviewer);
            return Save();  
        }
    }
}
