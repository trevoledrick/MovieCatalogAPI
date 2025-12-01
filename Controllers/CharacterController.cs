using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MovieholicAPI.Models.Domain;
using MovieholicAPI.Models.DTO.Character;
using MovieholicAPI.Services.CharacterServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MovieholicAPI.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharacterController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICharacterService characterService;

        public CharacterController(IMapper mapper, ICharacterService characterService)
        {
            this.mapper = mapper;
            this.characterService = characterService;
        }

        /// <summary>
        /// Displays all characters.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters()
        {
            return mapper.Map<List<CharacterReadDTO>>(await characterService.GetAllCharactersAsync());
        }

        /// <summary>
        /// Displays a character by Id.
        /// Returns NotFound() if character is null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
        {
            Character character = await characterService.GetCharacterByIdAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return mapper.Map<CharacterReadDTO>(character);
        }

        /// <summary>
        /// Updates a character by Id. Passing full character object and Id is required.
        /// Returns BadRequest() if id and dtoCharacter.Id are not equal.
        /// Returns NotFound() if character don't exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoCharacter"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterEditDTO dtoCharacter)
        {
            if (id != dtoCharacter.Id)
            {
                return BadRequest();
            }

            if (!characterService.CharacterExists(id))
            {
                return NotFound();
            }

            Character domainCharacter = mapper.Map<Character>(dtoCharacter);
            await characterService.UpdateCharacterAsync(domainCharacter);

            return NoContent();
        }

        /// <summary>
        /// Creates and adds new character to the database.
        /// </summary>
        /// <param name="dtoCharacter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO dtoCharacter)
        {
            Character domainCharacter = mapper.Map<Character>(dtoCharacter);

            domainCharacter = await characterService.AddNewCharacterAsync(domainCharacter);

            return CreatedAtAction("GetCharacter",
                new { id = domainCharacter.CharacterId },
                mapper.Map<CharacterCreateDTO>(domainCharacter));
        }

        /// <summary>
        /// Deletes a character by Id, from database. Irreversible action.
        /// Returns NotFound() if character don't exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!characterService.CharacterExists(id))
            {
                return NotFound();
            }

            await characterService.DeleteCharacterByIdAsync(id);

            return NoContent();
        }
    }
}