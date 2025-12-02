using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MovieCatalogAPI.Models.Domain;
using MovieCatalogAPI.Models.DTO.Character;
using MovieCatalogAPI.Models.DTO.Franchise;
using MovieCatalogAPI.Services.FranchiseServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieCatalogAPI.Controllers
{
    [Route("api/v1/franchises")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchiseController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IFranchiseService franchiseService;

        public FranchiseController(IMapper mapper, IFranchiseService franchiseService)
        {
            this.mapper = mapper;
            this.franchiseService = franchiseService;
        }

        /// <summary>
        /// Displays all franchises.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetFranchises()
        {
            return mapper.Map<List<FranchiseReadDTO>>(await franchiseService.GetAllFranchisesAsync());
        }

        /// <summary>
        /// Displays a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            Franchise franchise = await franchiseService.GetFranchiseByIdAsync(id);

            if (franchise == null)
            {
                return NotFound();
            }

            return mapper.Map<FranchiseReadDTO>(franchise);
        }

        /// <summary>
        /// Displays all movies in a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<FranchiseMoviesReadDTO>>> GetFranchiseMovies(int id)
        {
            return mapper.Map<List<FranchiseMoviesReadDTO>>(await franchiseService.GetAllMoviesFromFranchise(id));
        }

        /// <summary>
        /// Displays all characters in a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetFranchiseCharacters(int id)
        {
            return mapper.Map<List<CharacterReadDTO>>(await franchiseService.GetAllCharactersFromFranchise(id));
        }

        /// <summary>
        /// Updates a franchise by Id. Passing full franchise object and Id is required.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoFranchise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseEditDTO dtoFranchise)
        {
            if (id != dtoFranchise.Id)
            {
                return BadRequest();
            }

            if (!franchiseService.FranchiseExists(id))
            {
                return NotFound();
            }

            Franchise domainFranchise = mapper.Map<Franchise>(dtoFranchise);
            await franchiseService.UpdateFranchiseAsync(domainFranchise);

            return NoContent();
        }

        /// <summary>
        /// Updates movies in a franchise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        [HttpPut("{id}/UpdateMovies")]
        public async Task<IActionResult> PutFranchiseMovies(int id, List<int> characters)
        {
            if (!franchiseService.FranchiseExists(id))
            {
                return NotFound();
            }
            await franchiseService.UpdateFranchiseMoviesAsync(id, characters);

            return NoContent();
        }

        /// <summary>
        /// Creates and adds new franchise to the database.
        /// </summary>
        /// <param name="dtoFranchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO dtoFranchise)
        {
            Franchise domainFranchise = mapper.Map<Franchise>(dtoFranchise);

            domainFranchise = await franchiseService.AddNewFranchiseAsync(domainFranchise);

            return CreatedAtAction("GetFranchise",
                new { id = domainFranchise.Id },
                mapper.Map<FranchiseReadDTO>(domainFranchise));
        }

        /// <summary>
        /// Deletes a franchise by Id, from database. Irreversible action.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (!franchiseService.FranchiseExists(id))
            {
                return NotFound();
            }

            await franchiseService.DeleteFranchiseByIdAsync(id);

            return NoContent();
        }
    }
}