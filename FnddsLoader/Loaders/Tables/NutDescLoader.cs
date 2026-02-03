using System.Data.OleDb;
using FnddsData.Fndds.Models;
using FnddsData.FnddsLoader.Contexts;
using FnddsData.FnddsLoader.Entities;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsData.FnddsLoader.Loaders.Tables;

/// <summary>
/// This class contains functionaility for loading data for the nutrient
/// description table.
/// </summary>
public class NutDescLoader : DataLoader
{
    /// <summary>
    /// The table name in the source database.
    /// </summary>
    private const string SourceTableName = "NutDesc";

    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<NutDescLoader> _logger = new NLogLoggerFactory().CreateLogger<NutDescLoader>();

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Constructs a new NutDescLoader object.
    /// </summary>
    /// <param name="version">The FNDDS version.</param>
    /// <param name="connection">The connection to the source database.</param>
    /// <param name="context">The destination database context.</param>
    public NutDescLoader(FnddsVersion version, OleDbConnection connection, FnddsDbContext context)
        : base(version, connection, context)
    {
        _isDebugEnabled = _logger.IsEnabled(LogLevel.Debug);
    }

    /// <inheritdoc />
    public override IEnumerable<DataColumnModel> Columns =>
        [
            new DataColumnModel
            {
                SourceName = "[Nutrient code]",
                DestinationName = "NutrientCode",
                IsOrderedBy = true,
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[Nutrient description]",
                DestinationName = "NutrientDescription",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "Tagname",
                DestinationName = "Tagname",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "Unit",
                DestinationName = "Unit",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "Decimals",
                DestinationName = "Decimals",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
        ];

    /// <inheritdoc />
    public override string TableName => SourceTableName;

    /// <inheritdoc />
    public override async Task<int> CreateRecordsAsync(IEnumerable<DataColumnModel> columns, OleDbDataReader reader)
    {
        var nutrients = new List<NutDesc>();

        var recordCount = 0;
        while (reader.Read())
        {
            var nutrient = new NutDesc
            {
                VersionId = FnddsVersion.Id,
                CreateDt = DateTime.UtcNow
            };

            SetModelValues(columns, reader, nutrient);

            nutrients.Add(nutrient);

            if (_isDebugEnabled)
            {
                _logger.LogDebug("Table: {tableName}, Nutrient code: {nutrientCode}", SourceTableName,
                    nutrient.NutrientCode);
            }

            if (nutrients.Count > BatchSize)
            {
                Context.NutDescs.AddRange(nutrients);

                await Context.SaveChangesAsync();

                nutrients.Clear();
            }

            recordCount++;
        }

        Context.NutDescs.AddRange(nutrients);

        await Context.SaveChangesAsync();

        return recordCount;
    }
}
