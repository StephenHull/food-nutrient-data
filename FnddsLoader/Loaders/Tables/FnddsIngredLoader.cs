using System.Data.OleDb;
using FnddsData.Fndds.Models;
using FnddsData.FnddsLoader.Contexts;
using FnddsData.FnddsLoader.Entities;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsData.FnddsLoader.Loaders.Tables;

/// <summary>
/// This class contains functionaility for loading data for the FNDDS ingredient table.
/// </summary>
public class FnddsIngredLoader : DataLoader
{
    /// <summary>
    /// The table name in the source database.
    /// </summary>
    private const string SourceTableName = "FnddsIngred";

    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<FnddsIngredLoader> _logger =
        new NLogLoggerFactory().CreateLogger<FnddsIngredLoader>();

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Constructs a new FnddsIngredLoader object.
    /// </summary>
    /// <param name="version">The FNDDS version.</param>
    /// <param name="connection">The connection to the source database.</param>
    /// <param name="context">The destination database context.</param>
    public FnddsIngredLoader(FnddsVersion version, OleDbConnection connection, FnddsDbContext context)
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
                SourceName = "[Seq num]",
                DestinationName = "SeqNum",
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
                SourceName = "[SR code]",
                DestinationName = "IngredientCode",
                Versions = [1, 2, 4, 8, 16, 32, 64]
            },
            new DataColumnModel
            {
                SourceName = "[Ingredient code]",
                DestinationName = "IngredientCode",
                Versions = [128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[SR description]",
                DestinationName = "IngredientDescription",
                Versions = [1, 2, 4, 8, 16, 32, 64]
            },
            new DataColumnModel
            {
                SourceName = "[Ingredient description]",
                DestinationName = "IngredientDescription",
                Versions = [128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "Amount",
                DestinationName = "Amount",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "Measure",
                DestinationName = "Measure",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[Portion code]",
                DestinationName = "PortionCode",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[Retention code]",
                DestinationName = "RetentionCode",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "Flag",
                DestinationName = "Flag",
                Versions = [1, 2, 4, 8, 16, 32, 64]
            },
            new DataColumnModel
            {
                SourceName = "Weight",
                DestinationName = "IngredientWeight",
                Versions = [1, 2, 4, 8, 16, 32, 64]
            },
            new DataColumnModel
            {
                SourceName = "[Ingredient weight]",
                DestinationName = "IngredientWeight",
                Versions = [128, 256, 512, 1024]
            },
            new DataColumnModel
            {
                SourceName = "[Change type to SR Code]",
                DestinationName = "ChangeTypeToSRCode",
                Versions = [1, 2, 4, 8, 16, 32]
            },
            new DataColumnModel
            {
                SourceName = "[Change type to weight]",
                DestinationName = "ChangeTypeToWeight",
                Versions = [1, 2, 4, 8, 16, 32]
            },
            new DataColumnModel
            {
                SourceName = "[Change type to retn code]",
                DestinationName = "ChangeTypeToRetnCode",
                Versions = [1, 2, 4, 8, 16, 32]
            },
        ];

    /// <inheritdoc />
    public override string TableName => SourceTableName;

    /// <inheritdoc />
    public override async Task<int> CreateRecordsAsync(IEnumerable<DataColumnModel> columns, OleDbDataReader reader)
    {
        var srLinks = new List<FnddsIngred>();

        var recordCount = 0;
        while (reader.Read())
        {
            var srLink = new FnddsIngred
            {
                VersionId = FnddsVersion.Id,
                CreateDt = DateTime.UtcNow
            };

            SetModelValues(columns, reader, srLink);

            srLinks.Add(srLink);

            if (_isDebugEnabled)
            {
                _logger.LogDebug("Table: {tableName}, Food code: {foodCode}, Sequence: {sequence}", SourceTableName,
                    srLink.FoodCode, srLink.SeqNum);
            }

            if (srLinks.Count > BatchSize)
            {
                Context.FnddsIngreds.AddRange(srLinks);

                await Context.SaveChangesAsync();

                srLinks.Clear();
            }

            recordCount++;
        }

        if (srLinks.Count > 0)
        {
            Context.FnddsIngreds.AddRange(srLinks);

            await Context.SaveChangesAsync();
        }

        return recordCount;
    }
}
