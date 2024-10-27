using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Entities
{
    public class Movie : BaseEntity<int>
    {
        /// <summary>
        /// Id retreived from TMDB
        /// </summary>
        public int TMDBId { get; set; }
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

        // URL to TMDb page for the movie (can be used in the frontend)
        public string TmdbUrl => $"https://www.themoviedb.org/movie/{Id}";

        // Proxy indicator: determines if this movie data is cached or directly retrieved from the API
        public bool IsCached { get; set; } = false;

        // Collection navigation containing dependents
        public ICollection<Genre> Genres { get; } = new List<Genre>();

        // Navigation property for offline lists
        public ICollection<OfflineList> OfflineLists { get; set; }  

        public Movie() { }

        public Movie(
            int id
            , string title
            , string overview
            , string backdropPath
            , DateTime releaseDate
            , double voteAverage
            )
        {
            Id = id;
            Title = title;
            Overview = overview;
            BackdropPath = backdropPath;
            ReleaseDate = releaseDate;
            VoteAverage = voteAverage;
        }
    }
}
