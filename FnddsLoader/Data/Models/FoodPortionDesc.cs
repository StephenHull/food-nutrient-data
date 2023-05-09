using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FnddsLoader.Data.Models;

[Table("FoodPortionDesc")]
public partial class FoodPortionDesc
{
    public FoodPortionDesc()
    {
        FnddsIngred = new HashSet<FnddsIngred>();
        FoodWeights = new HashSet<FoodWeights>();
    }

    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PortionCode { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Version { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    [StringLength(120)]
    public string PortionDescription { get; set; }

    [StringLength(1)]
    public string ChangeType { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<FnddsIngred> FnddsIngred { get; set; }

    public virtual FnddsVersion FnddsVersion { get; set; }

    public virtual ICollection<FoodWeights> FoodWeights { get; set; }
}
