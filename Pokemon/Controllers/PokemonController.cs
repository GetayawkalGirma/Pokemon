
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;
using PokemonReviewer.Data.Dto;
using PokemonReviewer.Interfaces;
using PokemonReviewer.Repository;
using System.Xml.Linq;

namespace PokemonReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController:Controller
    {
        private readonly Ipokemonrepository _pokemonrepository;
        private readonly IcountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public PokemonController(Ipokemonrepository pokemonrepository,IcountryRepository countryRepository, IMapper mapper)
        {
            _pokemonrepository = pokemonrepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemonn>))]
        public IActionResult GetPokemons()
        {
            var pokemons=_mapper.Map<List<PokemonDto>>(_pokemonrepository.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200,Type = typeof(Pokemonn))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if(!_pokemonrepository.PokemonExists(pokeId))
                return NotFound();
            var pokemon =_mapper.Map<PokemonDto>(_pokemonrepository.GetPokemon(pokeId));
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemon);
        }
        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonrepository.PokemonExists(pokeId))
                return NotFound();

            var rating = _pokemonrepository.GetPokemonRating(pokeId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(rating);
        }
        [HttpPost]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonrepository.GetPokemons()
                .Where(pok => pok.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (pokemons != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemonn>(pokemonCreate);

            
            if (!_pokemonrepository.CreatePokemon(ownerId, catId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
