using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;
using PokemonReviewer.Data.Dto;
using PokemonReviewer.Interfaces;
using PokemonReviewer.Repository;

namespace PokemonReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController:Controller
    {
        private readonly IcountryRepository _countryrepository;
        private readonly IMapper _mapper;

        public CountryController(IcountryRepository countryrepository, IMapper mapper)
        {
            _countryrepository = countryrepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemonn>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryrepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(countries);
        }
        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryrepository.CountryExists(countryId))
                return NotFound();
            var Country=_mapper.Map<CountryDto>(_countryrepository.GetCountry(countryId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Country);
        }
        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            if (!_countryrepository.CountryExists(ownerId))
                return NotFound();
            var Owner=_mapper.Map<CountryDto>(
                _countryrepository.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Owner);  
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryDto countrycreate)
        {
            if (countrycreate == null)
                return BadRequest(ModelState);
            var country = _countryrepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countrycreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var countryMap = _mapper.Map<Country>(countrycreate);

            if (!_countryrepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok(countryMap);
        }
    }
}
