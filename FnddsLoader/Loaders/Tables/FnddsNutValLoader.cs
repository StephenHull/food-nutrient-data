using System.Data.OleDb;
using FnddsData.Fndds.Models;
using FnddsData.FnddsLoader.Contexts;
using FnddsData.FnddsLoader.Entities;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsData.FnddsLoader.Loaders.Tables;

/// <summary>
/// This class contains functionaility for loading data for the FNDDS nutrient
/// values table.
/// </summary>
public class FnddsNutValLoader : DataLoader
{
    /// <summary>
    /// The table name in the source database.
    /// </summary>
    private const string SourceTableName = "FnddsNutVal";

    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<FnddsNutValLoader> _logger =
        new NLogLoggerFactory().CreateLogger<FnddsNutValLoader>();

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Constructs a new FnddsNutValLoader object.
    /// </summary>
    /// <param name="version">The FNDDS version.</param>
    /// <param name="connection">The connection to the source database.</param>
    /// <param name="context">The destination database context.</param>
    public FnddsNutValLoader(FnddsVersion version, OleDbConnection connection, FnddsDbContext context)
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
                SourceName = "[Nutrient code]",
                DestinationName = "NutrientCode",
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
                SourceName = "[Nutrient value]",
                DestinationName = "NutrientValue",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
        ];

    /// <inheritdoc />
    public override string TableName => SourceTableName;

    /// <inheritdoc />
    public override async Task<int> CreateRecordsAsync(IEnumerable<DataColumnModel> columns, OleDbDataReader reader)
    {
        var nutrients = new List<FnddsNutVal>();

        var recordCount = 0;
        while (reader.Read())
        {
            var nutrient = new FnddsNutVal
            {
                VersionId = FnddsVersion.Id,
                CreateDt = DateTime.UtcNow
            };

            SetModelValues(columns, reader, nutrient);

            nutrients.Add(nutrient);

            if (_isDebugEnabled)
            {
                _logger.LogDebug("Table: {tableName}, Food code: {foodCode}, Nutrient code: {nutrientCode}",
                    SourceTableName, nutrient.FoodCode, nutrient.NutrientCode);
            }

            if (nutrients.Count > BatchSize)
            {
                Context.FnddsNutVals.AddRange(nutrients);

                await Context.SaveChangesAsync();

                nutrients.Clear();
            }

            recordCount++;
        }

        if (nutrients.Count > 0)
        {
            Context.FnddsNutVals.AddRange(nutrients);

            await Context.SaveChangesAsync();
        }

        return recordCount;
    }
}
