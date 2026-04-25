using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Vybaveni
{
    [Key]
    [Column("id_vybaveni")]
    public int IdVybaveni { get; set; }

    [Column("nazev")]
    public string Nazev { get; set; }

    [Column("popis")]
    public string? Popis { get; set; }

    [Column("cena_za_den")]
    public decimal CenaZaDen { get; set; }

    [Column("id_typ")]
    public int IdTyp { get; set; }

    [Column("id_stav")]
    public int IdStav { get; set; }

    [Column("obrazek_url")]
    public string? ObrazekUrl { get; set; }

    [Column("metadata")]
    public string? Metadata { get; set; }

    public TypVybaveni TypVybaveni { get; set; }
    public StavVybaveni StavVybaveni { get; set; }
}