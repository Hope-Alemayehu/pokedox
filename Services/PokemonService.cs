using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using pokedex.Models;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("PokedexDb");
            _pokemonCollection = database.GetCollection<Pokemon>("Pokemons");
        }

        public async Task<List<Pokemon>> GetPokemonsAsync()
        {
            return await _pokemonCollection.Find(_ => true).ToListAsync();
        }

        // Get Pokemon by Name, with case insensitivity
        public async Task<Pokemon> GetPokemonByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }

            var filter = Builders<Pokemon>.Filter.Regex("Name", new MongoDB.Bson.BsonRegularExpression($"^{name}$", "i"));
            var pokemon = await _pokemonCollection.Find(filter).FirstOrDefaultAsync();

            if (pokemon == null)
            {
                throw new Exception("Pokemon not found");
            }

            return pokemon;
        }

        // Update Pokemon by ID
        public async Task<Pokemon> UpdatePokemonAsync(string id, Pokemon updatedPokemon)
        {
            var result = await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);
            if (result.MatchedCount == 0)
            {
                throw new Exception("Pokemon not found");
            }
            return updatedPokemon;
        }

        // Delete Pokemon by ID
        public async Task<bool> DeletePokemonAsync(string id)
        {
            var result = await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("Pokemon not found");
            }
            return true;
        }
    }
}
