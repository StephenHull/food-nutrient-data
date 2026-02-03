using System.Data.OleDb;
using FnddsData.Fndds.Interfaces;
using FnddsData.FnddsLoader.Contexts;
using FnddsData.FnddsLoader.Entities;
using FnddsData.FnddsLoader.Loaders;
using FnddsData.FnddsLoader.Loaders.Tables;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsData.FnddsLoader;

public class FnddsLoader
{
    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<FnddsLoader> _logger = new NLogLoggerFactory().CreateLogger<FnddsLoader>();

    private readonly FnddsDbContext _dbContext;

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Constructs a new FNDDS loader.
    /// </summary>
    public FnddsLoader(FnddsDbContext dbContext)
    {
        _dbContext = dbContext;
        _isDebugEnabled = _logger.IsEnabled(LogLevel.Debug);
    }

    /// <summary>
    /// Imports data from a source database.
    /// </summary>
    /// <param name="fnddsVersion">The FNDDS version.</param>
    /// <param name="connString">The connection string for the source database.</param>
    /// <returns>Returns true if the method completes successfully.</returns>
    public async Task<bool> ImportDataAsync(IFnddsVersion fnddsVersion, string connString)
    {
        try
        {
            var version = _dbContext.FnddsVersions.SingleOrDefault(x => x.Id == fnddsVersion.Id);
            if (version != null)
            {
                _dbContext.FnddsVersions.Remove(version);

                await _dbContext.SaveChangesAsync();
            }

            version = new FnddsVersion
            {
                Id = fnddsVersion.Id,
                BeginYear = fnddsVersion.BeginYear,
                EndYear = fnddsVersion.EndYear,
                Major = fnddsVersion.Major,
                Minor = fnddsVersion.Minor,
                CreateDt = DateTime.Now
            };

            _dbContext.FnddsVersions.Add(version);

            await _dbContext.SaveChangesAsync();

            using var connection = new OleDbConnection(connString);

            await connection.OpenAsync();

            var loaders =
                new List<DataLoader>
                {
                    new DerivDescLoader(version, connection, _dbContext),
                    new FoodPortionDescLoader(version, connection, _dbContext),
                    new MainFoodDescLoader(version, connection, _dbContext),
                    new AddFoodDescLoader(version, connection, _dbContext),
                    new FnddsIngredLoader(version, connection, _dbContext),
                    new NutDescLoader(version, connection, _dbContext),
                    new FnddsNutValLoader(version, connection, _dbContext),
                    new FoodWeightLoader(version, connection, _dbContext),
                    new MoistNFatAdjustLoader(version, connection, _dbContext),
                    new IngredNutValLoader(version, connection, _dbContext),
                    new SubcodeDescLoader(version, connection, _dbContext),
                };

            foreach (var loader in loaders)
            {
                var recordsLoaded = await loader.LoadAsync();

                if (_isDebugEnabled)
                {
                    _logger.LogDebug("Table: {tableName}, Records: {recordCount}", loader.TableName, recordsLoaded);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to import the data.");

            throw;
        }
    }
}
