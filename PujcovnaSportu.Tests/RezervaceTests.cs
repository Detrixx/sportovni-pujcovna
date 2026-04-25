using Xunit;

namespace PujcovnaSportu.Tests
{
    public class RezervaceTests
    {
        [Fact]
        public void Rezervace_DatumOdJeDrivNezDatumDo()
        {
            var rez = new Rezervace
            {
                DatumOd = new DateTime(2025, 6, 1),
                DatumDo = new DateTime(2025, 6, 7)
            };

            Assert.True(rez.DatumOd < rez.DatumDo);
        }

        [Fact]
        public void Rezervace_DelkaRezervaceJeSestDni()
        {
            var rez = new Rezervace
            {
                DatumOd = new DateTime(2025, 6, 1),
                DatumDo = new DateTime(2025, 6, 7)
            };

            var delka = (rez.DatumDo - rez.DatumOd).Days;
            Assert.Equal(6, delka);
        }

        [Fact]
        public void RezervacePolozka_CelkovaCenaJeSpravna()
        {
            var polozka = new RezervacePolozka
            {
                Mnozstvi = 3,
                CenaZaDen = 200
            };

            var celkem = polozka.Mnozstvi * polozka.CenaZaDen;
            Assert.Equal(600, celkem);
        }

        [Fact]
        public void Rezervace_PoznamkaMuzeBytNull()
        {
            var rez = new Rezervace { Poznamka = null };
            Assert.Null(rez.Poznamka);
        }
    }
}
