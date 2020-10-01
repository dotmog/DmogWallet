using DmogWallet;
using NUnit.Framework;

namespace DmogWalletTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateWalletTest()
        {
            var wallet = new Wallet("1234");

            Assert.True(wallet.IsCreated);
        }
    }
}