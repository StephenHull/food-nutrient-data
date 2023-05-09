using Fndds.Interfaces;
using FnddsLoader.Data.Models;

namespace Fndds.Extensions;

/// <summary>
/// This class contains extension methods for an integer.
/// </summary>
public static class Int32Extensions
{
    /// <summary>
    /// Returns the FNDDS version that matches the specified ID (or null if a match
    /// cannot be found).
    /// </summary>
    /// <param name="id">The version ID.</param>
    /// <returns>The FNDDS version.</returns>
    public static IFnddsVersion GetVersion(this int id)
    {
        return id switch
        {
            1 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2001,
                EndYear = 2002,
                Major = 1,
                Minor = 0
            },
            2 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2003,
                EndYear = 2004,
                Major = 2,
                Minor = 0
            },
            4 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2005,
                EndYear = 2006,
                Major = 3,
                Minor = 0
            },
            8 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2007,
                EndYear = 2008,
                Major = 4,
                Minor = 1
            },
            16 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2009,
                EndYear = 2010,
                Major = 5,
                Minor = 0
            },
            32 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2011,
                EndYear = 2012,
                Major = 6,
                Minor = 0
            },
            64 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2013,
                EndYear = 2014,
                Major = 7,
                Minor = 0
            },
            128 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2015,
                EndYear = 2016,
                Major = 8,
                Minor = 0
            },
            256 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2017,
                EndYear = 2018,
                Major = 9,
                Minor = 0
            },
            512 => new FnddsVersion
            {
                Id = id,
                BeginYear = 2019,
                EndYear = 2020,
                Major = 10,
                Minor = 0
            },
            _ => default,
        };
    }
}
