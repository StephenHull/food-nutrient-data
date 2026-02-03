using System.Data.OleDb;
using FnddsData.Fndds.Models;
using FnddsData.FnddsLoader.Contexts;
using FnddsData.FnddsLoader.Entities;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsData.FnddsLoader.Loaders.Tables;

/// <summary>
/// This class contains functionaility for loading data for the main food
/// description table.
/// </summary>
public class MainFoodDescLoader : DataLoader
{
    /// <summary>
    /// The table name in the source database.
    /// </summary>
    private const string SourceTableName = "MainFoodDesc";

    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<MainFoodDescLoader> _logger =
        new NLogLoggerFactory().CreateLogger<MainFoodDescLoader>();

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Constructs a new MainFoodDescLoader object.
    /// </summary>
    /// <param name="version">The FNDDS version.</param>
    /// <param name="connection">The connection to the source database.</param>
    /// <param name="context">The destination database context.</param>
    public MainFoodDescLoader(FnddsVersion version, OleDbConnection connection, FnddsDbContext context)
        : base(version, connection, context)
    {
        _isDebugEnabled = _logger.IsEnabled(LogLevel.Debug);
    }

    /// <inheritdoc />
    public override IEnumerable<DataColumnModel> Columns =>
        [
            new DataColumnModel
            {
                SourceName = "[Food code]",
                DestinationName = "FoodCode",
                IsOrderedBy = true,
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[Start date]",
                DestinationName = "StartDT",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256]
            },
            new DataColumnModel
            {
                SourceName = "[End date]",
                DestinationName = "EndDT",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256]
            },
            new DataColumnModel
            {
                SourceName = "[Main food description]",
                DestinationName = "MainFoodDescription",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[Abbreviated description]",
                DestinationName = "AbbreviatedMainFoodDescription",
                Versions = [1, 2, 4]
            },
            new DataColumnModel
            {
                SourceName = "[Fortification identifier]",
                DestinationName = "FortificationIdentifier",
                Versions = [32]
            },
            new DataColumnModel
            {
                SourceName = "[Fortification identifier code]",
                DestinationName = "FortificationIdentifier",
                Versions = [128]
            },
            new DataColumnModel
            {
                SourceName = "[WWEIA Category code]",
                DestinationName = "CategoryNumber",
                Versions = [128]
            },
            new DataColumnModel
            {
                SourceName = "[WWEIA Category number]",
                DestinationName = "CategoryNumber",
                Versions = [256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[WWEIA Category description]",
                DestinationName = "CategoryDescription",
                Versions = [256, 512, 1024]
            },
        ];

    /// <inheritdoc />
    public override string TableName => SourceTableName;

    /// <inheritdoc />
    public override async Task<int> CreateRecordsAsync(IEnumerable<DataColumnModel> columns, OleDbDataReader reader)
    {
        var foods = new List<MainFoodDesc>();

        var recordCount = 0;
        while (reader.Read())
        {
            var food = new MainFoodDesc
            {
                VersionId = FnddsVersion.Id,
                CreateDt = DateTime.UtcNow
            };

            SetModelValues(columns, reader, food);

            foods.Add(food);

            if (_isDebugEnabled)
            {
                _logger.LogDebug("Table: {tableName}, Food code: {foodCode}", SourceTableName, food.FoodCode);
            }

            if (foods.Count > BatchSize)
            {
                Context.MainFoodDescs.AddRange(foods);

                await Context.SaveChangesAsync();

                foods.Clear();
            }

            recordCount++;
        }

        if (foods.Count > 0)
        {
            Context.MainFoodDescs.AddRange(foods);

            await Context.SaveChangesAsync();
        }

        return recordCount;
    }
}
