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
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _OwnerRepository;
        private readonly IcountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository OwnerRepository,IcountryRepository countryRepository ,IMapper mapper)
        {
            _OwnerRepository = OwnerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var Owners = _mapper.Map<List<OwnerDto>>(_OwnerRepository.GetOwners());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Owners);
        }
        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]

        public IActionResult Getowner(int ownerId)
        {
            if (!_OwnerRepository.OwnerExists(ownerId))
                return NotFound();
            var Owner = _mapper.Map<OwnerDto>(_OwnerRepository.GetOwner(ownerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Owner);

        }
        [HttpGet("pokemon/{ownername}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemonn>))]
        [ProducesResponseType(400)]
        public IActionResult Getpokemonbyownername(String ownername)
        {

            if (!_OwnerRepository.OwnerExists(ownername))
                return NotFound();
            var pokemon = _mapper.Map<PokemonDto>(_OwnerRepository.Getpokemonbyownername(ownername));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(pokemon);
        }
        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemonn>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_OwnerRepository.OwnerExists(ownerId))
                return NotFound();
            var Owner = _mapper.Map<List<PokemonDto>>(_OwnerRepository.GetOwnerByPokemon(ownerId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(Owner);

        }
        [HttpPost]
        public IActionResult CreateOwner([FromQuery] int countryId ,[FromBody] OwnerDto ownerpassed)
        {
            if (ownerpassed == null)
                return BadRequest(ModelState);
            //var ownerexist = _OwnerRepository.GetOwners().Where(ow => ow.FirstName.Trim().ToUpper() == ownerpassed.FirstName.Trim().ToUpper());
            var ownerexist = _OwnerRepository.GetOwners().FirstOrDefault(ow => ow.FirstName.Trim().ToUpper() == ownerpassed.FirstName.Trim().ToUpper());
            if (ownerexist != null)
            {
                ModelState.AddModelError("", "The Owner Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var OwnerMap = _mapper.Map<Owner>(ownerpassed);
            OwnerMap.Country = _countryRepository.GetCountry(countryId);
            if (!_OwnerRepository.CreateOwner(OwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);

            }
            return Ok(OwnerMap);
        }
    } 
}