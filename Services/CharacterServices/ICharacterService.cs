using System.Collections.Generic;
using System.Threading.Tasks;
using MovieholicAPI.Models.Domain;

namespace MovieholicAPI.Services.CharacterServices
{
    public interface ICharacterService
    {
        public Task<IEnumerable<Character>> GetAllCharactersAsync();
        public Task<Character> GetCharacterByIdAsync(int id);
        public Task<Character> AddNewCharacterAsync(Character domainCharacter);
        public Task UpdateCharacterAsync(Character domainCharacter);
        public Task DeleteCharacterByIdAsync(int id);
        public bool CharacterExists(int id);
    }
}