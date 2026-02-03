using System.Data.OleDb;
using FnddsData.Fndds.Models;
using FnddsData.FnddsLoader.Contexts;
using FnddsData.FnddsLoader.Entities;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsData.FnddsLoader.Loaders.Tables;

/// <summary>
/// This class contains functionaility for loading data for the subcode description
/// table.
/// </summary>
public class SubcodeDescLoader : DataLoader
{
    /// <summary>
    /// The table name in the source database.
    /// </summary>
    private const string SourceTableName = "SubcodeDesc";

    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<SubcodeDescLoader> _logger =
        new NLogLoggerFactory().CreateLogger<SubcodeDescLoader>();

    /// <summary>
    /// True if the logger is debug endabled; otherwise, false.
    /// </summary>
    private readonly bool _isDebugEnabled = false;

    /// <summary>
    /// Constructs a new SubcodeDescLoader object.
    /// </summary>
    /// <param name="version">The FNDDS version.</param>
    /// <param name="connection">The connection to the source database.</param>
    /// <param name="context">The destination database context.</param>
    public SubcodeDescLoader(FnddsVersion version, OleDbConnection connection, FnddsDbContext context)
        : base(version, connection, context)
    {
        _isDebugEnabled = _logger.IsEnabled(LogLevel.Debug);
    }

    /// <inheritdoc />
    public override IEnumerable<DataColumnModel> Columns =>
        [
            new DataColumnModel
            {
                SourceName = "[Subcode]",
                DestinationName = "Subcode",
                IsOrderedBy = true,
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256]
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
                SourceName = "[Subcode description]",
                DestinationName = "SubcodeDesc",
                Versions = [1, 2, 4, 8, 16, 32, 64, 128, 256]
            },
        ];

    /// <inheritdoc />
    public override string TableName => SourceTableName;

    /// <inheritdoc />
    public override async Task<int> CreateRecordsAsync(IEnumerable<DataColumnModel> columns, OleDbDataReader reader)
    {
        var subcodes = new List<SubcodeDesc>();

        var recordCount = 0;
        while (reader.Read())
        {
            var subcode = new SubcodeDesc
            {
                VersionId = FnddsVersion.Id,
                CreateDt = DateTime.UtcNow
            };

            SetModelValues(columns, reader, subcode);

            subcodes.Add(subcode);

            if (_isDebugEnabled)
            {
                _logger.LogDebug("Table: {tableName}, Subcode: {subcode}", SourceTableName, subcode.Subcode);
            }

            if (subcodes.Count > BatchSize)
            {
                Context.SubcodeDescs.AddRange(subcodes);

                await Context.SaveChangesAsync();

                subcodes.Clear();
            }

            recordCount++;
        }

        if (subcodes.Count > 0)
        {
            Context.SubcodeDescs.AddRange(subcodes);

            await Context.SaveChangesAsync();
        }

        return recordCount;
    }
}
