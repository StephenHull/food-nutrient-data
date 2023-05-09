using System;
using System.Threading.Tasks;
using Fndds.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace FnddsLoader;

/// <summary>
/// This class is a utility for loading FNDDS data from USDA into a database.
/// </summary>
public class Program
{
    /// <summary>
    /// The logger class.
    /// </summary>
    private static readonly ILogger<Program> _logger = new NLogLoggerFactory().CreateLogger<Program>();

    /// <summary>
    /// The main method. This method simply calls MainAsync.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <remarks>
    /// Arguments:
    ///     fnddsVersion {Integer} the FNDDS version
    ///         1   = FNDDS 1.0 (2001-2002)
    ///         2   = FNDDS 2.0 (2003-2004)
    ///         4   = FNDDS 3.0 (2005-2006)
    ///         8   = FNDDS 4.1 (2007-2008)
    ///         16  = FNDDS 5.0 (2009-2010)
    ///         32  = FNDDS 2011-2012
    ///         64  = FNDDS 2013-2014
    ///         128 = FNDDS 2015-2016
    ///         256 = FNDDS 2017-2018
    ///         512 = FNDDS 2019-2020
    ///     connString {String} the FNDDS Access database OLEDB connection string
    ///         Example: Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:/databases/FNDDS.mdb;Persist Security Info=False;
    /// </remarks>
    public static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    /// <summary>
    /// The async main method. This method is where the work is done.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static async Task MainAsync(string[] args)
    {
        if (args.Length < 2)
        {
            _logger.LogCritical("Missing command-line arguments.");

            Environment.Exit(0);
        }

        _logger.LogDebug("FNDDS Version ID: {fnddsVersionId}", args[0]);
        _logger.LogDebug("Local Connection String: {connectionString}", args[1]);

        var id = -1;
        var connString = string.Empty;

        try
        {
            id = Convert.ToInt32(args[0]);
            connString = args[1];
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Failed to parse the command-line arguments.");
        }

        var version = id.GetVersion();
        if (version == null)
        {
            _logger.LogCritical("Invalid FNDDS version.");

            Environment.Exit(0);
        }

        var loader = new FnddsLoader();

        await loader.ImportDataAsync(version, connString);
    }
}
