using MovieApp.Domain.Entities;
using MovieApp.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Persistence;

public interface IUnitOfWork
{
    IMovieRepository Movies { get; }
    IGenreRepository Genres { get; }
    IOfflineListRepository OfflineLists { get; }

    Task<int> CommitAsync();
}
