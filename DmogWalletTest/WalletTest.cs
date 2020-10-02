using DmogWallet;
using NUnit.Framework;
using System;
using System.IO;

namespace DmogWalletTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            SystemInteraction.ReadData = f => File.ReadAllText(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), f));
            SystemInteraction.DataExists = f => File.Exists(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), f));
            SystemInteraction.ReadPersistent = f => File.ReadAllText(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), f));
            SystemInteraction.PersistentExists = f => File.Exists(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), f));
            SystemInteraction.Persist = (f, c) => File.WriteAllText(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), f), c);
        }

        [Test]
        public void CreateWalletTest()
        {
            var wallet = new Wallet("1234", "wallet.dat");

            Assert.True(wallet.IsCreated);
        }
    }
}