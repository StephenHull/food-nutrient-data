using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Fndds.Interfaces;

namespace FnddsLoader.Data.Models;

[Table("FnddsVersion")]
public class FnddsVersion : IFnddsVersion
{
    public FnddsVersion()
    {
        DerivDesc = new HashSet<DerivDesc>();
        FoodPortionDesc = new HashSet<FoodPortionDesc>();
        MainFoodDesc = new HashSet<MainFoodDesc>();
        NutDesc = new HashSet<NutDesc>();
        SubcodeDesc = new HashSet<SubcodeDesc>();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public int BeginYear { get; set; }

    public int EndYear { get; set; }

    public int? Major { get; set; }

    public int? Minor { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<DerivDesc> DerivDesc { get; set; }

    public virtual ICollection<FoodPortionDesc> FoodPortionDesc { get; set; }

    public virtual ICollection<MainFoodDesc> MainFoodDesc { get; set; }

    public virtual ICollection<NutDesc> NutDesc { get; set; }

    public virtual ICollection<SubcodeDesc> SubcodeDesc { get; set; }
}
