using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieApp.Business.Interfaces;
using MovieApp.Business.Dtos;
using System.Net;
using System.Security.Claims;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ITMDbService _tmdbService;

        public MovieController(IMovieService movieService, ITMDbService tmdbService)
        {
            _movieService = movieService;
            _tmdbService = tmdbService;
        }

        /// <summary>
        /// Gets the list of popular movies.
        /// </summary>
        /// <param name="jwtToken">The JWT token for authentication.</param>
        /// <returns>A list of popular movies.</returns>
        [HttpGet("popular")]
        //[Authorize] // Ensures the user is authenticated
        public async Task<IActionResult> GetPopularMovies([FromHeader] string jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return BadRequest("JWT token is required.");
            }

            try
            {
                var movies = await _movieService.GetPopularMoviesAsync(jwtToken);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error retrieving popular movies: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the details of a specific movie by ID.
        /// </summary>
        /// <param name="movieId">The ID of the movie.</param>
        /// <param name="jwtToken">The JWT token for authentication.</param>
        /// <returns>Details of the specified movie.</returns>
        [HttpGet("{movieId}")]
        [Authorize] // Ensures the user is authenticated
        public async Task<IActionResult> GetMovieDetails(int movieId, [FromHeader] string jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return BadRequest("JWT token is required.");
            }

            try
            {
                var movieDetails = await _movieService.GetMovieDetailsAsync(movieId, jwtToken);
                if (movieDetails == null)
                {
                    return NotFound($"Movie with ID {movieId} not found.");
                }
                return Ok(movieDetails);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error retrieving movie details: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a movie to the offline list.
        /// </summary>
        /// <param name="movieId">The ID of the movie to add.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost("offline/{movieId}")]
        [Authorize] // Ensures the user is authenticated
        public async Task<IActionResult> AddMovieToOfflineList(int movieId)
        {
            // Retrieve the User ID from the JWT claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User identifier not found.");
            }

            // Call the service to add the movie to the offline list
            bool success = await _movieService.AddMovieToOfflineListAsync(movieId, userId);
            if (success)
            {
                return Ok($"Movie with ID {movieId} added to offline list.");
            }
            return BadRequest($"Failed to add movie with ID {movieId} to offline list.");
        }
    }
}
