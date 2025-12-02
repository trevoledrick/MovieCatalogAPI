using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCatalogAPI.Models;
using MovieCatalogAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MovieCatalogAPI.Services.FranchiseServices
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MoviesContext context;

        public FranchiseService(MoviesContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all franchises.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await context.Franchises.ToListAsync();
        }

        /// <summary>
        /// Gets a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Franchise> GetFranchiseByIdAsync(int id)
        {
            return await context.Franchises.FindAsync(id);
        }

        /// <summary>
        /// Gets all movies in a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Franchise>> GetAllMoviesFromFranchise(int id)
        {
            return await context.Franchises
                .Include(c => c.Movies)
                .Where(c => c.Id == id)
                .ToListAsync();
        }

        /// <summary>
        /// Gets all characters in a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> GetAllCharactersFromFranchise(int id)
        {
            var franchiseMovies = await context.Movies
            .Include(c =>c.Characters)
            .Where(c =>c.FranchiseId == id)
            .ToListAsync();
            // var franchiseMovies = context.Movies.Include(c => c.Characters)
            //                                     .Where(c => c.FranchiseId == id);

            var chararacterList = new List<Character>();
            var seenIds = new HashSet<int>();
            foreach (var movie in franchiseMovies)
            {
                foreach (var character in movie.Characters)
                {
                    if (seenIds.Add(character.CharacterId))
                    {
                        chararacterList.Add(character);
                    }
                }
            }

            return chararacterList;
        }

        /// <summary>
        /// Adds a franchise to the database.
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        public async Task<Franchise> AddNewFranchiseAsync(Franchise franchise)
        {
            context.Franchises.Add(franchise);
            await context.SaveChangesAsync();
            return franchise;
        }

        /// <summary>
        /// Updates a franchise by Id.
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            context.Entry(franchise).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a movie in a franchise by Id.
        /// Throws a KeyNotFoundException if movie is null.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movies"></param>
        /// <returns></returns>
        public async Task UpdateFranchiseMoviesAsync(int id, List<int> movies)
        {
            Franchise updateFranchiseMovies = await context.Franchises
                .Include(c => c.Movies)
                .Where(c => c.Id == id)
                .FirstAsync();

            List<Movie> moviesList = new();
            foreach (int movieId in movies)
            {
                Movie movie = await context.Movies.FindAsync(movieId);

                if (movie == null)
                    throw new KeyNotFoundException();

                moviesList.Add(movie);
            }

            updateFranchiseMovies.Movies = moviesList;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteFranchiseByIdAsync(int id)
        {
            var franchise = await context.Franchises.FindAsync(id);
            context.Franchises.Remove(franchise);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the franchise exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool FranchiseExists(int id)
        {
            return context.Franchises.Any(e => e.Id == id);
        }
    }
}