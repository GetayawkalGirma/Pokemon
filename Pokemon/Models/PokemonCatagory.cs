namespace Pokemon.Models
{
    //  This are Join Tables

    public class PokemonCatagory
    {
        public int PokemonId { get; set; }
        public int CatagoryId { get; set; }
        public Pokemonn Pokemon { get; set; }
        public Catagory Catagory { get; set; }
        

    }
}
