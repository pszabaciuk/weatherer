using Weatherer.Server.Domain;

namespace Weatherer.Server.Persistence;

public interface ICityRepository
{
    IEnumerable<City> GetAll();
}