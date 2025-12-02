using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCatalogAPI.Models.Domain;

namespace MovieCatalogAPI.Services.FranchiseServices
{
    public interface IFranchiseService
    {
        public Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
        public Task<Franchise> GetFranchiseByIdAsync(int id);
        public Task<IEnumerable<Franchise>> GetAllMoviesFromFranchise(int id);
        public Task<IEnumerable<Character>> GetAllCharactersFromFranchise(int id);
        public Task<Franchise> AddNewFranchiseAsync(Franchise domainFranchise);
        public Task UpdateFranchiseAsync(Franchise domainFranchise);
        public Task UpdateFranchiseMoviesAsync(int id, List<int> movies);
        public Task DeleteFranchiseByIdAsync(int id);
        public bool FranchiseExists(int id);
    }
}