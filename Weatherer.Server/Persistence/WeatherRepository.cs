using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using RepoDb;
using Weatherer.Config;
using Weatherer.Server.Domain;
using Weatherer.Server.DTOs;

namespace Weatherer.Server.Persistence;

internal sealed class WeatherRepository : BaseRepository<Domain.Weather, SqliteConnection>, IWeatherRepository
{
    private readonly string _connectionString;

    public WeatherRepository(IOptions<DatabaseOptions> options) : base(options.Value.ConnectionString)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public void Add(Weather weather)
    {
        Insert(weather);
    }

    public async Task<IEnumerable<WeatherDataItem>> GetMaxWindAsync(CancellationToken ct)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = @"SELECT c.Country, c.Name AS City, MIN(w.Temperature) AS Value, MAX(w.Timestamp) AS Timestamp
                        FROM Weather w
                        INNER JOIN City c ON c.CityId = w.CityId
                        GROUP BY c.Country, c.Name";

            var xx = connection.ExecuteReader(sql);

            return await connection.ExecuteQueryAsync<WeatherDataItem>(sql, ct);
        }
    }

    public async Task<IEnumerable<WeatherDataItem>> GetMinTemperaturesAsync(CancellationToken ct)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string sql = @"SELECT c.Country, c.Name AS City, MAX(w.WindSpeed) AS Value, MAX(w.Timestamp) AS Timestamp
                        FROM Weather w
                        INNER JOIN City c ON c.CityId = w.CityId
                        GROUP BY c.Country, c.Name";

            return await connection.ExecuteQueryAsync<WeatherDataItem>(sql, ct);
        }
    }

    public async Task<IEnumerable<WeatherDataItem>> GetTempTrendAsync(string city, CancellationToken ct)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            long lastTwoHoursTimestamp = DateTimeOffset.UtcNow.AddHours(-2).ToUnixTimeSeconds();

            string sql = @$"SELECT w.Temperature AS Value, w.Timestamp, c.Name AS City, c.Country
                        FROM Weather w
                        JOIN City c ON c.CityId = w.CityId
                        WHERE c.Name = '{city}'
                        AND W.Timestamp >= {lastTwoHoursTimestamp}
                        ORDER BY W.Timestamp ASC;";

            return await connection.ExecuteQueryAsync<WeatherDataItem>(sql, ct);
        }
    }

    public async Task<IEnumerable<WeatherDataItem>> GetWindTrendAsync(string city, CancellationToken ct)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            long lastTwoHoursTimestamp = DateTimeOffset.UtcNow.AddHours(-2).ToUnixTimeSeconds();

            string sql = @$"SELECT w.WindSpeed AS Value, w.Timestamp, c.Name AS City, c.Country
                        FROM Weather w
                        JOIN City c ON c.CityId = w.CityId
                        WHERE c.Name = '{city}'
                        AND W.Timestamp >= {lastTwoHoursTimestamp}
                        ORDER BY W.Timestamp ASC;";

            return await connection.ExecuteQueryAsync<WeatherDataItem>(sql, ct);
        }
    }
}
