using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FnddsLoader.Data.Models;

[Table("NutDesc")]
public partial class NutDesc
{
    public NutDesc()
    {
        FnddsNutVal = new HashSet<FnddsNutVal>();
        IngredNutVal = new HashSet<IngredNutVal>();
    }

    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int NutrientCode { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Version { get; set; }

    [Required]
    [StringLength(45)]
    public string NutrientDescription { get; set; }

    [StringLength(15)]
    public string Tagname { get; set; }

    [Required]
    [StringLength(10)]
    public string Unit { get; set; }

    public int Decimals { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<FnddsNutVal> FnddsNutVal { get; set; }

    public virtual FnddsVersion FnddsVersion { get; set; }

    public virtual ICollection<IngredNutVal> IngredNutVal { get; set; }
}
