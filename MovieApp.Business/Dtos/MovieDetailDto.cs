using MovieApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Dtos
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string BackdropPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double VoteAverage { get; set; }
        public double? Rating { get; set; }
        public string? OriginalLanguage { get; set; }
        public double? Popularity { get; set; }
        public int? VoteCount { get; set; }
        public int? Runtime { get; set; }
        public decimal? Budget { get; set; }
        public int? Revenue { get; set; }
        public List<GenreDto> Genres { get; set; }
    }
}
