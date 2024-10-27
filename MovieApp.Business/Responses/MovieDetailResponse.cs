using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Responses
{
    public class MovieDetailResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string BackdropPath { get; set; }
        public string ReleaseDate { get; set; } // TMDB returns ReleaseDate as a string
        public double VoteAverage { get; set; }
        public double? Rating { get; set; }
        public string? OriginalLanguage { get; set; }
        public double? Popularity { get; set; }
        public int? VoteCount { get; set; }
        public int? Runtime { get; set; }
        public decimal? Budget { get; set; }
        public int? Revenue { get; set; }
        public List<GenreResponse> Genres { get; set; }
    }
}
