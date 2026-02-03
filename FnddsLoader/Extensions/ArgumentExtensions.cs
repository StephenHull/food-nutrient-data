using System.Text.RegularExpressions;
using FnddsData.FnddsLoader.Entities;
using FnddsData.FnddsLoader.Models;

namespace FnddsData.FnddsLoader.Extensions;

public static partial class ArgumentExtensions
{
    [GeneratedRegex("Microsoft\\.ACE\\.OLEDB\\.16\\.0;Data Source=.+\\.mdb;Persist Security Info=False;")]
    private static partial Regex ConnectionStringRegex();

    public static LoaderArguments GetArguments(this string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Error! No command-line arguments were specified.");
            Console.WriteLine("Required arguments:");
            Console.WriteLine("    FnddsVersion: <int> - The version of FNDDS to load.");
            Console.WriteLine("           1 = FNDDS 1.0 (2001-2002)");
            Console.WriteLine("           2 = FNDDS 2.0 (2003-2004)");
            Console.WriteLine("           4 = FNDDS 3.0 (2005-2006)");
            Console.WriteLine("           8 = FNDDS 4.1 (2007-2008)");
            Console.WriteLine("          16 = FNDDS 5.0 (2009-2010)");
            Console.WriteLine("          32 = FNDDS 2011-2012");
            Console.WriteLine("          64 = FNDDS 2013-2014");
            Console.WriteLine("         128 = FNDDS 2015-2016");
            Console.WriteLine("         256 = FNDDS 2017-2018");
            Console.WriteLine("         512 = FNDDS 2019-2020");
            Console.WriteLine("        1024 = FNDDS 2021-2023");
            Console.WriteLine("    ConnectionString: <string> - The Access database connection string.");
            Console.WriteLine("        Example: Provider=Microsoft.ACE.OLEDB.16.0;Data Source=c:/FNDDS.mdb;Persist Security Info=False;");
            Console.WriteLine("Usage: FnddsData.Loader <FnddsVersion> <ConnectionString>");

            throw new ArgumentException("No command-line arguments were specified.");
        }

        if (!FnddsVersion.TryParse(args[0], out var version) || version == null)
        {
            throw new ArgumentException("The FNDDS Version was invalid.");
        }

        if (!ConnectionStringRegex().IsMatch(args[1]))
        {
            throw new ArgumentException("The Connection String was invalid.");
        }

        return new LoaderArguments
        {
            FnddsVersion = version,
            ConnectionString = args[1]
        };
    }
}
