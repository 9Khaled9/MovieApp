using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Responses
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string BackdropPath { get; set; }
        public string ReleaseDate { get; set; } // TMDB returns ReleaseDate as a string
        public double VoteAverage { get; set; }
    }
}
