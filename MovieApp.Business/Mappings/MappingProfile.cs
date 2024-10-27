using AutoMapper;
using MovieApp.Business.Dtos;
using MovieApp.Business.Responses;
using MovieApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Movie to MovieDto mapping
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TMDBId));

            // Movie to MovieDetailDto mapping
            CreateMap<Movie, MovieDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TMDBId));
            //.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()));

            // Genre to GenreDto mapping
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TMDBId));

            // MovieResponse to MovieDto mapping
            CreateMap<MovieResponse, MovieDto>();

            // MovieDetailResponse to MovieDetailDto mapping
            CreateMap<MovieDetailResponse, MovieDetailDto>()
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateTime.Parse(src.ReleaseDate))); // Custom string to DateTime conversion

            // GenreResponse to GenreDto mapping
            CreateMap<GenreResponse, GenreDto>();
        }
    }
}
