using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class StavVybaveni
{
    [Key]
    [Column("id_stav")]
    public int IdStav { get; set; }

    [Column("nazev")]
    public string Nazev { get; set; }
}