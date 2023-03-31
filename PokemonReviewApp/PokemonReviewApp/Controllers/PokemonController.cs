using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.DTOS;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Collections.Generic;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemon _pokemonInterface;
        private readonly IMapper _mapper;

        public PokemonController(IPokemon pokemon, IMapper mapper)
        {
            _pokemonInterface = pokemon;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDTO>>(_pokemonInterface.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonById(int pokeId)
        {
            if(!_pokemonInterface.PokemonExist(pokeId))
            {
                return NotFound();
            }

            var pokemon =   _mapper.Map<PokemonDTO>(_pokemonInterface.GetPokemonById(pokeId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetRating(int pokeId)
        {
            if(!_pokemonInterface.PokemonExist(pokeId))
            {
                return NotFound();
            }

            var rating =_pokemonInterface.GetPokemonRating(pokeId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rating);
        }
    }
}
