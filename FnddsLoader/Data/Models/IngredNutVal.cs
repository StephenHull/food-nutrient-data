using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FnddsLoader.Data.Models;

[Table("IngredNutVal")]
public class IngredNutVal
{
    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int IngredientCode { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int NutrientCode { get; set; }

    [Key]
    [Column(Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Version { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    [StringLength(200)]
    public string IngredientDescription { get; set; }

    public decimal NutrientValue { get; set; }

    [Required]
    [StringLength(80)]
    public string NutrientValueSource { get; set; }

    public int? FdcId { get; set; }

    [StringLength(4)]
    public string DerivationCode { get; set; }

    public int? SrAddModYear { get; set; }

    public int? FoundationYearAcquired { get; set; }

    public DateTime Created { get; set; }

    public virtual NutDesc NutDesc { get; set; }
}
