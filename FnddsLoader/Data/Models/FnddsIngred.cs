using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FnddsLoader.Data.Models;

[Table("FnddsIngred")]
public partial class FnddsIngred
{
    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int FoodCode { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int SeqNum { get; set; }

    [Key]
    [Column(Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int IngredientCode { get; set; }

    [Key]
    [ForeignKey(nameof(FoodPortionDesc))]
    [Column(Order = 4)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Version { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    [StringLength(255)]
    public string IngredientDescription { get; set; }

    public decimal Amount { get; set; }

    [StringLength(3)]
    public string Measure { get; set; }

    [ForeignKey(nameof(FoodPortionDesc))]
    [Column(Order = 3)]
    public int? PortionCode { get; set; }

    public int? RetentionCode { get; set; }

    public int? Flag { get; set; }

    public decimal IngredientWeight { get; set; }

    [StringLength(1)]
    public string ChangeTypeToSrCode { get; set; }

    [StringLength(1)]
    public string ChangeTypeToWeight { get; set; }

    [StringLength(1)]
    public string ChangeTypeToRetnCode { get; set; }

    public DateTime Created { get; set; }

    public virtual FoodPortionDesc FoodPortionDesc { get; set; }

    public virtual MainFoodDesc MainFoodDesc { get; set; }
}
