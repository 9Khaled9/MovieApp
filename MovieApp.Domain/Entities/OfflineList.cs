using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Entities
{
    public class OfflineList
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }  // Assuming the UserId is a string identifier from the authentication provider.

        public Movie Movie { get; set; }  
    }
}
