using MovieApp.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Interfaces
{
    public interface ITMDbService
    {
        Task<List<MovieDto>> GetPopularMoviesAsync(string jwtToken);
        Task<MovieDetailDto> GetMovieDetailsAsync(int movieId, string jwtToken);
    }
}
