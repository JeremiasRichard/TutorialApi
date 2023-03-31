using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.DTOS;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountry _country;
        private readonly IMapper _mapper;

        public CountryController(ICountry country, IMapper mapper)
        {
            _country = country;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDTO>>(_country.GetCountries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryById(int countryId)
        {
            if (!_country.CountryExist(countryId))
            {
                return NotFound();
            }

            var country = _mapper.Map<CountryDTO>(_country.GetCountry(countryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {   
            var country = _mapper.Map<CountryDTO>(_country.GetCountryByOwner(ownerId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

    }
}
