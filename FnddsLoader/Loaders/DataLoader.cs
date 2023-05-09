using System.Data.OleDb;
using Fndds.Interfaces;
using Fndds.Services;
using FnddsLoader.Data;
using FnddsLoader.Data.Models;

namespace FnddsLoader.Loaders;

public abstract class DataLoader : BaseDataLoader
{
    /// <summary>
    /// Constructs a new DataLoader object.
    /// </summary>
    /// <param name="version">The FNDDS version.</param>
    /// <param name="connection">The connection to the source database.</param>
    /// <param name="context">The destination database context.</param>
    protected DataLoader(FnddsVersion version, OleDbConnection connection, FnddsContext context)
    {
        FnddsVersion = version;
        Connection = connection;
        Context = context;
    }

    /// <summary>
    /// The source database connection.
    /// </summary>
    public sealed override OleDbConnection Connection { get; set; }

    /// <summary>
    /// The destination database context.
    /// </summary>
    public FnddsContext Context { get; }

    /// <summary>
    /// The FNDDS version.
    /// </summary>
    public sealed override IFnddsVersion FnddsVersion { get; set; }
}
