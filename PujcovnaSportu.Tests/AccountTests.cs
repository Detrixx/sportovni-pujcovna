using Xunit;

namespace PujcovnaSportu.Tests
{
    public class AccountTests
    {
        [Fact]
        public void HashHeslo_StejneHesloMaStejnyHash()
        {
            var hash1 = HesloHelper.HashHeslo("heslo123");
            var hash2 = HesloHelper.HashHeslo("heslo123");

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void HashHeslo_RuznaHeslaMajiRuzneHashe()
        {
            var hash1 = HesloHelper.HashHeslo("heslo123");
            var hash2 = HesloHelper.HashHeslo("jineheslo");

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void HashHeslo_VysledekNeniPrazdny()
        {
            var hash = HesloHelper.HashHeslo("cokoliv");
            Assert.False(string.IsNullOrEmpty(hash));
        }
    }
}
