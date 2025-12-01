using System.Collections.Generic;
using System.Threading.Tasks;
using MovieholicAPI.Models.Domain;

namespace MovieholicAPI.Services.MovieServices
{
    public interface IMovieService
    {
        public Task<IEnumerable<Movie>> GetAllMoviesAsync();
        public Task<Movie> GetMovieByIdAsync(int id);
        public Task<IEnumerable<Movie>> GetAllCharactersFromMovie(int id);
        public Task<Movie> AddNewMovieAsync(Movie domainMovie);
        public Task UpdateMovieAsync(Movie domainMovie);
        public Task UpdateMovieCharactersAsync(int id, List<int> characters);
        public Task DeleteMovieByIdAsync(int id);
        public bool MovieExists(int id);
    }
}