using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Entities
{
    public class Genre : BaseEntity<int>
    {
        /// <summary>
        /// Id retreived from TMDB
        /// </summary>
        public int TMDBId { get; set; }
        public string Name { get; set; }
    }
}
