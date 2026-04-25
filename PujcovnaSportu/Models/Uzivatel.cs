using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Uzivatel
{
    [Key]
    [Column("id_uzivatel")]
    public int IdUzivatel { get; set; }

    [Column("jmeno")]
    public string Jmeno { get; set; }

    [Column("prijmeni")]
    public string Prijmeni { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("heslo_hash")]
    public string HesloHash { get; set; }

    [Column("telefon")]
    public string Telefon { get; set; }

    [Column("datum_registrace")]
    public DateTime DatumRegistrace { get; set; }

    [Column("id_role")]
    public int IdRole { get; set; }

    public Role Role { get; set; }
}