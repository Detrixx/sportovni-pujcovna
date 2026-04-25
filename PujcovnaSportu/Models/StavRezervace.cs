using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class StavRezervace
{
    [Key]
    [Column("id_stav_rez")]
    public int IdStavRez { get; set; }

    [Column("nazev")]
    public string Nazev { get; set; }
}