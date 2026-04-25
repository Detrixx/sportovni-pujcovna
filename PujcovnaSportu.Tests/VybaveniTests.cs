using Xunit;

namespace PujcovnaSportu.Tests
{
    public class VybaveniTests
    {
        [Fact]
        public void Vybaveni_NazevJeSpravneNastaveny()
        {
            var v = new Vybaveni { Nazev = "Horské kolo" };
            Assert.Equal("Horské kolo", v.Nazev);
        }

        [Fact]
        public void Vybaveni_CenaZaDenJeKladna()
        {
            var v = new Vybaveni { CenaZaDen = 150 };
            Assert.True(v.CenaZaDen > 0);
        }

        [Fact]
        public void Vybaveni_PopisMuzeBytNull()
        {
            var v = new Vybaveni { Nazev = "Lyže", Popis = null };
            Assert.Null(v.Popis);
        }

        [Fact]
        public void Vybaveni_TypVybaveniJeSpravneNavazany()
        {
            var typ = new TypVybaveni { IdTyp = 1, Nazev = "Zimní sporty" };
            var v = new Vybaveni { IdTyp = 1, TypVybaveni = typ };

            Assert.Equal("Zimní sporty", v.TypVybaveni.Nazev);
        }
    }
}
