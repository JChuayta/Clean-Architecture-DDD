using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Infraestructure.Persistence;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Infraestructure.Repositories
{
    public class StreamerRepository : RepositoryBase<Streamer>, IStreamerRepository
    {
        public StreamerRepository(StreamerDbContext context) : base(context)
        {
        }
    }
}
