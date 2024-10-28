﻿namespace Pokemon.Models
{
    public class Pokemonn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public ICollection<PokemonCatagory> PokemonCatagories { get; set; }
            
    }
}
