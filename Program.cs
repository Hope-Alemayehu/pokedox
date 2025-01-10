using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using pokedex.Models;
using pokedex.Services;

namespace Pokedex
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mongoDbService = new MongoDbService();
            Console.WriteLine("Connected to MongoDB...");

            // Add test Pokémon
            await AddSamplePokemons(mongoDbService);

            // Test querying Pokémon by name
            Console.WriteLine("Enter a Pokémon name:");
            string? name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid input. Pokémon name cannot be empty.");
            }
            else
            {
                var pokemon = await mongoDbService.GetPokemonByNameAsync(name);

                if (pokemon != null)
                {
                    Console.WriteLine($"Found: {pokemon.Name} - Type: {pokemon.Type} - HP: {pokemon.HP}");
                }
                else
                {
                    Console.WriteLine("Pokémon not found!");
                }
            }
        }

        // Add sample Pokémon data to the database
        private static async Task AddSamplePokemons(MongoDbService mongoDbService)
        {
            var samplePokemons = new List<Pokemon>
            {
                new Pokemon { Name = "Pikachu", Type = "Electric", Ability = "Static", Level = 25, HP = 100 },
                new Pokemon { Name = "Bulbasaur", Type = "Grass/Poison", Ability = "Overgrow", Level = 5, HP = 50 },
                new Pokemon { Name = "Charmander", Type = "Fire", Ability = "Blaze", Level = 15, HP = 60 },
                new Pokemon { Name = "Squirtle", Type = "Water", Ability = "Torrent", Level = 10, HP = 55 }
            };

            foreach (var pokemon in samplePokemons)
            {
                await mongoDbService.AddPokemonAsync(pokemon);
            }
        }
    }
}
