namespace FnddsLoader.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("DerivDesc")]
public partial class DerivDesc
{
    [Key]
    [Column(Order = 0)]
    [StringLength(4)]
    public string DerivationCode { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Version { get; set; }

    [Required]
    [StringLength(120)]
    public string DerivationDescription { get; set; }

    public DateTime Created { get; set; }

    public virtual FnddsVersion FnddsVersion { get; set; }
}
