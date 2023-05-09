using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FnddsLoader.Data.Models;

[Table("SubcodeDesc")]
public partial class SubcodeDesc
{
    public SubcodeDesc()
    {
        FoodWeights = new HashSet<FoodWeights>();
    }

    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Subcode { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Version { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    [StringLength(80)]
    public string SubcodeDescription { get; set; }

    public DateTime Created { get; set; }

    public virtual FnddsVersion FnddsVersion { get; set; }

    public virtual ICollection<FoodWeights> FoodWeights { get; set; }
}
