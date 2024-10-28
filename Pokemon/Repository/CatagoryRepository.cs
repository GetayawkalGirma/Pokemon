using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data;
using Pokemon.Models;
using PokemonReviewer.Data.Dto;
using PokemonReviewer.Interfaces;
using PokemonReviewer.Repository;

namespace PokemonReviewer.Repository
{
    public class CatagoryRepository : ICatagoryRepository
    {
        private readonly DataContext _context;

        public CatagoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CatagoryExists(int id)
        {
            return _context.Catagories.Any(c => c.Id == id);
        }

        public ICollection<Catagory> GetCatagories()
        {
            return _context.Catagories.ToList();

        }
        public Catagory GetCatagory(int id)
        {
            return _context.Catagories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemonn> GetPokemonByCatagoryId(int catagoryId)
        {
            return _context.pokemonCatagories.Where(e => e.CatagoryId == catagoryId).Select(c => c.Pokemon).ToList();

        }

        public bool CreateCatagory(Catagory catagory)
        {
            _context.Add(catagory);

            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}