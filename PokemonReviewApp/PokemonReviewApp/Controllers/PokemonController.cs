using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.DTOS;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;
using System.Collections.Generic;
using System.Transactions;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemon _pokemon;
        private readonly IMapper _mapper;
        private readonly IReview _review;

        public PokemonController(IPokemon pokemon,
            IMapper mapper, IReview review)
        {
            _pokemon = pokemon;
            _mapper = mapper;
            _review = review;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDTO>>(_pokemon.GetPokemons());

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
            if(!_pokemon.PokemonExist(pokeId))
            {
                return NotFound();
            }

            var pokemon =   _mapper.Map<PokemonDTO>(_pokemon.GetPokemonById(pokeId));

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
            if(!_pokemon.PokemonExist(pokeId))
            {
                return NotFound();
            }

            var rating =_pokemon.GetPokemonRating(pokeId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId,[FromQuery] int categoryId, PokemonDTO pokemonCreate)
        {
            if (pokemonCreate == null)
            {
                return BadRequest();
            }
            var pokemons = _pokemon.GetPokemons()
                .Where(x => x.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemons != null)
            {
                ModelState.AddModelError("", "Pokemon already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);



            if (!_pokemon.CreatePokemon(ownerId, categoryId,pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokemonId,[FromQuery] int ownerId,
            [FromQuery] int categoryId, PokemonDTO pokemonUpdate)
        {
            if (pokemonUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (pokemonId != pokemonUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_pokemon.PokemonExist(pokemonId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);

            if (!_pokemon.UpdatePokemon(ownerId,categoryId,pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokemonId)
        {
            if (!_pokemon.PokemonExist(pokemonId))
            {
                return NotFound();
            }
            var reviewsToDelete = _review.GetReviewsOfAPokemon(pokemonId);
            var pokemonToDelete = _pokemon.GetPokemonById(pokemonId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_review.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleteting reviews");
            }

            if (!_pokemon.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleteting pokemon");
            }

            return NoContent();
        }
    }
}

