using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string BackdropPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double VoteAverage { get; set; }
    }
}
