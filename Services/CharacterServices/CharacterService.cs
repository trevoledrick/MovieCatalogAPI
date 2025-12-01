using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieholicAPI.Models;
using MovieholicAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MovieholicAPI.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
        private readonly MoviesContext context;

        public CharacterService(MoviesContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await context.Characters.ToListAsync();
        }

        /// <summary>
        /// Gets a character by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await context.Characters.FindAsync(id);
        }

        /// <summary>
        /// Adds a character to the database.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public async Task<Character> AddNewCharacterAsync(Character character)
        {
            context.Characters.Add(character);
            await context.SaveChangesAsync();
            return character;
        }

        /// <summary>
        /// Updates a character by Id.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public async Task UpdateCharacterAsync(Character character)
        {
            context.Entry(character).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a character by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCharacterByIdAsync(int id)
        {
            var character = await context.Characters.FindAsync(id);
            context.Characters.Remove(character);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the character exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CharacterExists(int id)
        {
            return context.Characters.Any(e => e.CharacterId == id);
        }
    }
}