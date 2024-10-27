using MovieApp.Domain.Entities;
using MovieApp.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Persistence.Repository
{
    public class OfflineListRepository : Repository<OfflineList>, IOfflineListRepository
    {
        public OfflineListRepository(MovieAppDbContext context) : base(context) { }
    }
}
