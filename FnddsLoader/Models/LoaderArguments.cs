using FnddsData.FnddsLoader.Entities;

namespace FnddsData.FnddsLoader.Models;

public class LoaderArguments
{
    public FnddsVersion FnddsVersion { get; set; } = default!;

    public string ConnectionString { get; set; } = default!;
}
