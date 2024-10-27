using AutoMapper;
using MovieApp.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Services
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public List<TDestination> MapList<TSource, TDestination>(List<TSource> sourceList)
        {
            return _mapper.Map<List<TDestination>>(sourceList);
        }
    }
}
