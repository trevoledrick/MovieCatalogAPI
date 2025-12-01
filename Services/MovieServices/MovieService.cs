using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieholicAPI.Models;
using MovieholicAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MovieholicAPI.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly MoviesContext context;

        public MovieService(MoviesContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await context.Movies.ToListAsync();
        }

        /// <summary>
        /// Gets a movie by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await context.Movies.FindAsync(id);
        }

        /// <summary>
        /// Gets all characters in a movie by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetAllCharactersFromMovie(int id)
        {
            return await context.Movies
                .Include(c => c.Characters)
                .Where(c => c.MovieId == id)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a movie to the database.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task<Movie> AddNewMovieAsync(Movie movie)
        {
            context.Movies.Add(movie);
            await context.SaveChangesAsync();
            return movie;
        }

        /// <summary>
        /// Updates a movie by Id.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task UpdateMovieAsync(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a character in a movie by Id.
        /// Throws a KeyNotFoundException if character is null.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public async Task UpdateMovieCharactersAsync(int id, List<int> characters)
        {
            Movie updateMovieCharacters = await context.Movies
                .Include(c => c.Characters)
                .Where(c => c.MovieId == id)
                .FirstAsync();

            List<Character> chararacterList = new();
            foreach (int characterId in characters)
            {
                Character character = await context.Characters.FindAsync(characterId);

                if (character == null)
                    throw new KeyNotFoundException();

                chararacterList.Add(character);
            }

            updateMovieCharacters.Characters = chararacterList;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a movie by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteMovieByIdAsync(int id)
        {
            var movie = await context.Movies.FindAsync(id);
            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the movie exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MovieExists(int id)
        {
            return context.Movies.Any(e => e.MovieId == id);
        }
    }
}