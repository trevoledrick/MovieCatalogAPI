using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MovieholicAPI.Models.Domain;
using MovieholicAPI.Models.DTO.Movie;
using MovieholicAPI.Services.MovieServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieholicAPI.Controllers
{
    [Route("api/v1/movies")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MovieController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMovieService movieService;

        public MovieController(IMapper mapper, IMovieService movieService)
        {
            this.mapper = mapper;
            this.movieService = movieService;
        }

        /// <summary>
        /// Displays all movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
        {
            return mapper.Map<List<MovieReadDTO>>(await movieService.GetAllMoviesAsync());
        }

        /// <summary>
        /// Displays a movie by Id.
        /// Returns NotFound() if movie is null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
        {
            Movie movie = await movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return mapper.Map<MovieReadDTO>(movie);
        }

        /// <summary>
        /// Displays all characters in a specific movie by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/movie/characters")]
        public async Task<ActionResult<IEnumerable<MovieCharactersReadDTO>>> GetMovieCharacters(int id)
        {
            return mapper.Map<List<MovieCharactersReadDTO>>(await movieService.GetAllCharactersFromMovie(id));
        }

        /// <summary>
        /// Updates a movie by Id. Passing full movie object and Id is required.
        /// Returns BadRequest() if movieId and dtoMovie.Id is not equal.
        /// Returns NotFound() if the movie don't exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoMovie"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieEditDTO dtoMovie)
        {
            if (id != dtoMovie.Id)
            {
                return BadRequest();
            }

            if (!movieService.MovieExists(id))
            {
                return NotFound();
            }

            Movie domainMovie = mapper.Map<Movie>(dtoMovie);
            await movieService.UpdateMovieAsync(domainMovie);

            return NoContent();
        }

        /// <summary>
        /// Updates characters in a movie by Id.
        /// Returns NotFound() if the movie don't exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        [HttpPut("{id}/UpdateCharacters")]
        public async Task<IActionResult> PutMovieCharacter(int id, List<int> characters)
        {
            if (!movieService.MovieExists(id))
            {
                return NotFound();
            }
            await movieService.UpdateMovieCharactersAsync(id, characters);

            return NoContent();
        }

        /// <summary>
        /// Creates and adds new movie to the database.
        /// </summary>
        /// <param name="dtoMovie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDTO dtoMovie)
        {
            Movie domainMovie = mapper.Map<Movie>(dtoMovie);

            domainMovie = await movieService.AddNewMovieAsync(domainMovie);

            return CreatedAtAction("GetMovie",
                new { id = domainMovie.MovieId },
                mapper.Map<MovieReadDTO>(domainMovie));
        }

        /// <summary>
        /// Deletes a movie by Id, from database. Irreversible action.
        /// Returns NotFound() if the movie don't exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!movieService.MovieExists(id))
            {
                return NotFound();
            }

            await movieService.DeleteMovieByIdAsync(id);

            return NoContent();
        }
    }
}