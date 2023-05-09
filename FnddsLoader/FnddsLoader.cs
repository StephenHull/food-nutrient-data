using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using Fndds.Interfaces;
using FnddsLoader.Data;
using FnddsLoader.Data.Models;
using FnddsLoader.Loader.Tables;
using FnddsLoader.Loaders;
using FnddsLoader.Loaders.Tables;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsLoader;

public class FnddsLoader
{
    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<FnddsLoader> _logger = new NLogLoggerFactory().CreateLogger<FnddsLoader>();

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Gets or sets the connection string for the destination database.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Constructs a new FNDDS loader.
    /// </summary>
    public FnddsLoader()
    {
        _isDebugEnabled = _logger.IsEnabled(LogLevel.Debug);

        // Get the connection string
        var settings = ConfigurationManager.ConnectionStrings["Fndds"];
        if (settings != null)
        {
            ConnectionString = settings.ConnectionString;
        }
    }

    /// <summary>
    /// Imports data from a source database.
    /// </summary>
    /// <param name="fnddsVersion">The FNDDS version.</param>
    /// <param name="connString">The connection string for the source database.</param>
    /// <returns>Returns true if the method completes successfully.</returns>
    public async Task<bool> ImportDataAsync(IFnddsVersion fnddsVersion, string connString)
    {
        using var context = new FnddsContext();

        var version = context.FnddsVersion.SingleOrDefault(x => x.Id == fnddsVersion.Id);
        if (version != null)
        {
            context.FnddsVersion.Remove(version);

            await context.SaveChangesAsync();
        }

        version = new FnddsVersion
        {
            Id = fnddsVersion.Id,
            BeginYear = fnddsVersion.BeginYear,
            EndYear = fnddsVersion.EndYear,
            Major = fnddsVersion.Major,
            Minor = fnddsVersion.Minor,
            Created = DateTime.Now
        };

        context.FnddsVersion.Add(version);

        await context.SaveChangesAsync();

        using var connection = new OleDbConnection(connString);

        await connection.OpenAsync();

        var loaders =
            new List<DataLoader>
            {
                new DerivDescLoader(version, connection, context),
                new FoodPortionDescLoader(version, connection, context),
                new MainFoodDescLoader(version, connection, context),
                new AddFoodDescLoader(version, connection, context),
                new FnddsIngredLoader(version, connection, context),
                new NutDescLoader(version, connection, context),
                new FnddsNutValLoader(version, connection, context),
                new FoodWeightsLoader(version, connection, context),
                new MoistNFatAdjustLoader(version, connection, context),
                new IngredNutValLoader(version, connection, context),
                new SubcodeDescLoader(version, connection, context),
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
}
