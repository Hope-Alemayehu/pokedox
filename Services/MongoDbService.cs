using MongoDB.Bson;
using MongoDB.Driver;
using System;
using pokedex.Models;
using System.Threading.Tasks;

namespace Pokedex
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Pokemon> _pokemons;

        public MongoDbService()
        {
            var client = new MongoClient("mongodb://localhost:27017"); // MongoDB connection string
            var database = client.GetDatabase("pokedexDB");
            _pokemons = database.GetCollection<Pokemon>("pokemons");

            // Check MongoDB connection
            Console.WriteLine("Connected to MongoDB...");
        }

        public async Task<Pokemon?> GetPokemonByNameAsync(string name)
        {
            return await _pokemons.Find(pokemon => pokemon.Name == name).FirstOrDefaultAsync();
        }

        // Add a new Pokemon to the DB
        public async Task AddPokemonAsync(Pokemon pokemon)
        {
            await _pokemons.InsertOneAsync(pokemon);
        }
    }
}
