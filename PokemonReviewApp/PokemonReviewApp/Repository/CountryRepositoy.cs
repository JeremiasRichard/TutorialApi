using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepositoy : ICountry
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CountryRepositoy(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool CountryExist(int Id)
        {
            return _dbContext.Countries.Any(c => c.Id == Id);
        }

        public bool CreateCountry(Country country)
        {
            _dbContext.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _dbContext.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _dbContext.Countries.ToList();
        }

        public Country GetCountry(int Id)
        {
            return _dbContext.Countries.Where(x => x.Id == Id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _dbContext.Owners.Where(x => x.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _dbContext.Owners.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool Save()
        {   
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
            
        }

        public bool UpdateCountry(Country country)
        {
            _dbContext.Update(country);
            return Save();
        }
    }
}
