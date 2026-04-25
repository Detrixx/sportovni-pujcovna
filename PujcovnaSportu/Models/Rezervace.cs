using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Rezervace
{
    [Key]
    [Column("id_rezervace")]
    public int IdRezervace { get; set; }

    [Column("id_uzivatel")]
    public int IdUzivatel { get; set; }

    [Column("datum_vytvoreni")]
    public DateTime DatumVytvoreni { get; set; }

    [Column("datum_od")]
    public DateTime DatumOd { get; set; }

    [Column("datum_do")]
    public DateTime DatumDo { get; set; }

    [Column("id_stav_rez")]
    public int IdStavRez { get; set; }

    [Column("poznamka")]
    public string? Poznamka { get; set; }
}