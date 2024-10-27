using Microsoft.EntityFrameworkCore;
using MovieApp.Business.Dtos;
using MovieApp.Business.Interfaces;
using MovieApp.Caching.Interfaces;
using MovieApp.Domain.Entities;
using MovieApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITMDbService _tmdbService;
        private readonly ICacheService _cacheService;
        private readonly IMapperService _mapperService;

        public MovieService(ITMDbService tmdbService, ICacheService cacheService, IMapperService mapperService, IUnitOfWork unitOfWork)
        {
            _tmdbService = tmdbService;
            _cacheService = cacheService;
            _mapperService = mapperService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddMovieToOfflineListAsync(int movieId, string userId)
        {
            var exist = (await _unitOfWork.OfflineLists.FindAsync(x => x.UserId == userId && x.MovieId == movieId)).FirstOrDefault();
            if (exist == null)
            {
                var toSave = new OfflineList
                {
                    MovieId = movieId,
                    UserId = userId
                };
                await _unitOfWork.OfflineLists.AddAsync(toSave);
                await _unitOfWork.CommitAsync();
            }

            return true;
        }

        public async Task<MovieDetailDto> GetMovieDetailsAsync(int movieId, string jwtToken)
        {
            string cacheKey = $"movie_details_{movieId}";
            return await _cacheService.GetOrCreateAsync(cacheKey, async () =>
            {
                var movieEntity = (await _unitOfWork.Movies.FindAsync(x => x.TMDBId == movieId)).FirstOrDefault();
                if (movieEntity != null)
                {
                    return _mapperService.Map<Movie, MovieDetailDto>(movieEntity);
                }
                else
                {
                    var movieDetailDto = await _tmdbService.GetMovieDetailsAsync(movieId, jwtToken);
                    Movie toSave = new Movie(movieDetailDto.Id, movieDetailDto.Title, movieDetailDto.Overview, movieDetailDto.BackdropPath, movieDetailDto.ReleaseDate, movieDetailDto.VoteAverage);
                    toSave.Rating = movieDetailDto.Rating;
                    toSave.OriginalLanguage = movieDetailDto.OriginalLanguage;
                    toSave.Popularity = movieDetailDto.Popularity;
                    toSave.VoteCount = movieDetailDto.VoteCount;
                    toSave.Runtime = movieDetailDto.Runtime;
                    toSave.Budget = movieDetailDto.Budget;
                    toSave.Revenue = movieDetailDto.Revenue;
                    if (movieDetailDto.Genres is not null && movieDetailDto.Genres.Count > 0)
                    {
                        foreach (var genreDto in movieDetailDto.Genres)
                        {
                            if (genreDto.Id != null)
                            {
                                Genre genre = (await _unitOfWork.Genres.FindAsync(x => x.TMDBId == genreDto.Id)).FirstOrDefault();
                                if (genre is null)
                                {
                                    genre = new Genre
                                    {
                                        TMDBId = genreDto.Id.Value,
                                        Name = genreDto.Name ?? "Unnamed_Genre"
                                    };
                                    await _unitOfWork.Genres.AddAsync(genre);
                                }
                                toSave.Genres.Add(genre);
                            }
                        }
                    }
                    await _unitOfWork.Movies.AddAsync(toSave);
                    await _unitOfWork.CommitAsync();
                    return movieDetailDto;
                }
            });
        }

        public async Task<List<MovieDto>> GetPopularMoviesAsync(string jwtToken)
        {
            string cacheKey = "popular_movies";
            return await _cacheService.GetOrCreateAsync(cacheKey, async () =>
            {
                //Comparing between local data and retreived from TMDB
                var movieEntitiesCount = await _unitOfWork.Movies.GetTotalCountAsync();
                var movieDtos = await _tmdbService.GetPopularMoviesAsync(jwtToken);

                if (movieEntitiesCount == movieDtos.Count)
                {
                    var movieEntities = await _unitOfWork.Movies.GetAllAsync();
                    return _mapperService.MapList<Movie, MovieDto>(movieEntities.ToList());
                }
                else
                {
                    foreach (var dto in movieDtos)
                    {
                        var existingMovie = (await _unitOfWork.Movies.FindAsync(x => x.TMDBId == dto.Id)).FirstOrDefault();
                        if (existingMovie == null)
                        {
                            var movieDetailDto = await _tmdbService.GetMovieDetailsAsync(dto.Id, jwtToken);
                            Movie toSave = new Movie(dto.Id, dto.Title, dto.Overview, dto.BackdropPath, dto.ReleaseDate, dto.VoteAverage);
                            toSave.Rating = movieDetailDto.Rating;
                            toSave.OriginalLanguage = movieDetailDto.OriginalLanguage;
                            toSave.Popularity = movieDetailDto.Popularity;
                            toSave.VoteCount = movieDetailDto.VoteCount;
                            toSave.Runtime = movieDetailDto.Runtime;
                            toSave.Budget = movieDetailDto.Budget;
                            toSave.Revenue = movieDetailDto.Revenue;
                            if (movieDetailDto.Genres is not null && movieDetailDto.Genres.Count > 0)
                            {
                                foreach (var genreDto in movieDetailDto.Genres)
                                {
                                    if (genreDto.Id != null)
                                    {
                                        Genre genre = (await _unitOfWork.Genres.FindAsync(x => x.TMDBId == genreDto.Id)).FirstOrDefault();
                                        if (genre is null)
                                        {
                                            genre = new Genre
                                            {
                                                TMDBId = genreDto.Id.Value,
                                                Name = genreDto.Name ?? "Unnamed_Genre"
                                            };
                                            await _unitOfWork.Genres.AddAsync(genre);
                                        }
                                        toSave.Genres.Add(genre);
                                    }
                                }
                            }

                            await _unitOfWork.Movies.AddAsync(toSave);
                        }
                    }
                    await _unitOfWork.CommitAsync();
                    return movieDtos;
                }
            });
        }
    }
}
