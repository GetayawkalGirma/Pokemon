namespace Pokemon.Models
    // This are Join Tables
{
    public class PokemonOwner
    {
        public int PokemonId { get; set; }
        public int  OwnerId { get; set; }
        public Pokemonn Pokemon { get; set; }
        public Owner Owner { get; set; }

    }

}
