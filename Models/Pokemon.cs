using System;

namespace pokedex.Models
{
    public class Pokemon
    {
        public string? Id { get; set; }  
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Ability { get; set; }
        public int? Level { get; set; }
        public int? HP { get; set; }
    }
}
