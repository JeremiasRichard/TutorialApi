using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.DTOS;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        private readonly IMapper _mapper;

        public CategoryController(ICategory category, IMapper mapper)
        {
            _category = category;
             _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDTO>>(_category.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryById(int categoryId)
        {
            if (!_category.CategoryExist(categoryId))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDTO>(_category.GetCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDTO>>
                (_category.GetPokemonByCategoryId(categoryId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }
    }
}
