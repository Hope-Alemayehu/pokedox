using Microsoft.AspNetCore.Mvc;
using pokedex.Models;
using pokedex.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pokemon>>> GetAll()
    {
        var pokemons = await _pokemonService.GetPokemonsAsync();
        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pokemon>> GetById(string id)
    {
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
        if (pokemon == null)
        {
            return NotFound();
        }
        return Ok(pokemon);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Pokemon newPokemon)
    {
        var createdPokemon = await _pokemonService.AddPokemonAsync(newPokemon);
        return CreatedAtAction(nameof(GetById), new { id = createdPokemon.Id }, createdPokemon);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, Pokemon updatedPokemon)
    {
        try
        {
            await _pokemonService.UpdatePokemonAsync(id, updatedPokemon);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _pokemonService.DeletePokemonAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
