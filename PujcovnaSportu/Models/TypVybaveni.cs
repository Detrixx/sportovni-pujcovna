using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TypVybaveni
{
    [Key]
    [Column("id_typ")]
    public int IdTyp { get; set; }

    [Column("nazev")]
    public string Nazev { get; set; }
}