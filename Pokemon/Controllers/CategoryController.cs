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
    public class CategoryController : Controller
    {
        private readonly ICatagoryRepository _catagoryrepository;
        private readonly IMapper _mapper;

        public CategoryController(ICatagoryRepository catagoryrepository, IMapper mapper)
        {
            _catagoryrepository = catagoryrepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Catagory>))]
        public IActionResult GetCatagories()
        {
            var pokemons = _mapper.Map<List<CatagoryDto>>(_catagoryrepository.GetCatagories());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemons);
        }
        [HttpGet("{catagoryId}")]
        [ProducesResponseType(200, Type = typeof(Catagory))]
        [ProducesResponseType(400)]
        public IActionResult GetCatagory(int catagoryId)
        {
            if (!_catagoryrepository.CatagoryExists(catagoryId))
                return NotFound();
            var cata = _mapper.Map<CatagoryDto>(_catagoryrepository.GetCatagory(catagoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cata);

        }
        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemonn>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByCatagoryId(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_catagoryrepository.GetPokemonByCatagoryId(categoryId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(pokemons);

        }
        [HttpPost]
        public IActionResult Createcatagory([FromBody] CatagoryDto catagorycreate)
        {
            if (catagorycreate == null)
                return BadRequest(ModelState);
            var catagory = _catagoryrepository.GetCatagories()
                .Where(c => c.Name.Trim().ToUpper() == catagorycreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (catagory != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categoryMap = _mapper.Map<Catagory>(catagorycreate);

            if (!_catagoryrepository.CreateCatagory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok(categoryMap);
        }
    }
    
}

