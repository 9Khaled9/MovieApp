using Microsoft.Extensions.Configuration;
using MovieApp.Business.Dtos;
using MovieApp.Business.Interfaces;
using MovieApp.Business.Responses;
using MovieApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Services
{
    public class TMDbService : ITMDbService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMapperService _mapperService;

        public TMDbService(HttpClient httpClient, IConfiguration configuration, IMapperService mapperService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _mapperService = mapperService;
        }

        public async Task<List<MovieDto>> GetPopularMoviesAsync(string jwtToken)
        {
            var requestUri = $"{_configuration["TMDb:ApiBaseUrl"]}/movie/popular";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve popular movies from TMDb.");
            }

            var responseData = await response.Content.ReadFromJsonAsync<List<MovieResponse>>();
            return _mapperService.MapList<MovieResponse, MovieDto>(responseData);
        }

        public async Task<MovieDetailDto> GetMovieDetailsAsync(int movieId, string jwtToken)
        {
            var requestUri = $"{_configuration["TMDb:ApiBaseUrl"]}/movie/{movieId}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve movie details from TMDb.");
            }

            var responseData = await response.Content.ReadFromJsonAsync<MovieDetailResponse>();
            return _mapperService.Map<MovieDetailResponse, MovieDetailDto>(responseData);
        }
    }
}
