using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;
using System.Net.Mime;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwner
    {
        private readonly ApplicationDbContext _dbContext;
        public OwnerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public bool CreateOwner(Owner owner)
        {
            _dbContext.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _dbContext.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return _dbContext.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId)
        {
            return _dbContext.PokemonOwners.Where(p => p.Pokemon.Id == pokemonId)
                              .Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _dbContext.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return _dbContext.PokemonOwners.Where(p => p.Owner.Id == ownerId)
                .Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _dbContext.Owners.Any(x => x.Id == ownerId);
        }

        public bool Save()
        {
           var saved = _dbContext.SaveChanges();
            return saved > 0 ?  true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _dbContext.Update(owner);
            return Save();
        }
    }
        
}

