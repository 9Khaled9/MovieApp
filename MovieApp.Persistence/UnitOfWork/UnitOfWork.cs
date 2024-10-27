using MovieApp.Domain.Entities;
using MovieApp.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieAppDbContext _context;

        public IMovieRepository Movies { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IOfflineListRepository OfflineLists { get; private set; }

        public UnitOfWork(MovieAppDbContext context, IMovieRepository movieRepository, IGenreRepository genreRepository)
        {
            _context = context;
            Movies = movieRepository;
            Genres = genreRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
