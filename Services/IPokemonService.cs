using System.Collections.Generic;
using System.Threading.Tasks;
using pokedex.Models;

namespace pokedex.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetPokemonsAsync();  // Get all Pokémon

        // Declare the method to get Pokémon by name
        Task<Pokemon> GetPokemonByNameAsync(string name);  // Get Pokémon by Name

        Task<Pokemon> UpdatePokemonAsync(string id, Pokemon updatedPokemon);  // Update a Pokémon

        Task<bool> DeletePokemonAsync(string id);  // Delete a Pokémon
    }
}
