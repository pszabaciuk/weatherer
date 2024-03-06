using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using RepoDb;
using Weatherer.Config;
using Weatherer.Server.Domain;

namespace Weatherer.Server.Persistence;

internal sealed class CityRepository : BaseRepository<City, SqliteConnection>, ICityRepository
{
    public CityRepository(IOptions<DatabaseOptions> options) : base(options.Value.ConnectionString)
    {
    }

    public IEnumerable<City> GetAll()
    {
        return QueryAll();
    }
}
