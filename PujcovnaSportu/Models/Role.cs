using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Role
{
    [Key]
    [Column("id_role")]
    public int IdRole { get; set; }

    [Column("nazev")]
    public string Nazev { get; set; }
}