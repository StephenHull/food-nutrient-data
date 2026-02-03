using System.Data.OleDb;
using FnddsData.Fndds.Models;
using FnddsData.FnddsLoader.Contexts;
using FnddsData.FnddsLoader.Entities;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsData.FnddsLoader.Loaders.Tables;

/// <summary>
/// This class contains functionaility for loading data for the additional food
/// description table.
/// </summary>
public class AddFoodDescLoader : DataLoader
{
    /// <summary>
    /// The table name in the source database.
    /// </summary>
    private const string SourceTableName = "AddFoodDesc";

    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<AddFoodDescLoader> _logger =
        new NLogLoggerFactory().CreateLogger<AddFoodDescLoader>();

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Constructs a new AddFoodDescLoader object.
    /// </summary>
    /// <param name="version">The FNDDS version.</param>
    /// <param name="connection">The connection to the source database.</param>
    /// <param name="context">The destination database context.</param>
    public AddFoodDescLoader(FnddsVersion version, OleDbConnection connection, FnddsDbContext context)
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
                SourceName = "[Additional food description]",
                DestinationName = "AdditionalFoodDescription",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024]
            },
        ];

    /// <inheritdoc />
    public override string TableName => SourceTableName;

    /// <inheritdoc />
    public override async Task<int> CreateRecordsAsync(IEnumerable<DataColumnModel> columns, OleDbDataReader reader)
    {
        var descriptions = new List<AddFoodDesc>();

        var recordCount = 0;
        while (reader.Read())
        {
            var description = new AddFoodDesc
            {
                VersionId = FnddsVersion.Id,
                CreateDt = DateTime.UtcNow
            };

            SetModelValues(columns, reader, description);

            descriptions.Add(description);

            if (_isDebugEnabled)
            {
                _logger.LogDebug("Table: {tableName}, Food code: {foodCode}, Sequence: {sequenceNumber}",
                    SourceTableName, description.FoodCode, description.SeqNum);
            }

            if (descriptions.Count > BatchSize)
            {
                Context.AddFoodDescs.AddRange(descriptions);

                await Context.SaveChangesAsync();

                descriptions.Clear();
            }

            recordCount++;
        }

        if (descriptions.Count > 0)
        {
            Context.AddFoodDescs.AddRange(descriptions);

            await Context.SaveChangesAsync();
        }

        return recordCount;
    }
}
