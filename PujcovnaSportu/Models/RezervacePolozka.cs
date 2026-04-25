using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RezervacePolozka
{
    [Key]
    [Column("id_rezervace")]
    public int IdRezervace { get; set; }

    [Column("id_vybaveni")]
    public int IdVybaveni { get; set; }

    [Column("mnozstvi")]
    public int Mnozstvi { get; set; }

    [Column("cena_za_den")]
    public decimal CenaZaDen { get; set; }
}