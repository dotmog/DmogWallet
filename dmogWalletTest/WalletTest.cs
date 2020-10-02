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
            SystemInteraction.ReadData = f => File.ReadAllText(Path.Combine(Path.GetDirectoryName(typeof(Tests).Assembly.Location), f));
            SystemInteraction.DataExists = f => File.Exists(Path.Combine(Path.GetDirectoryName(typeof(Tests).Assembly.Location), f));
            SystemInteraction.ReadPersistent = f => File.ReadAllText(Path.Combine(Path.GetDirectoryName(typeof(Tests).Assembly.Location), f));
            SystemInteraction.PersistentExists = f => File.Exists(Path.Combine(Path.GetDirectoryName(typeof(Tests).Assembly.Location), f));
            SystemInteraction.Persist = (f, c) => File.WriteAllText(Path.Combine(Path.GetDirectoryName(typeof(Tests).Assembly.Location), f), c);
        }

        [Test]
        public void CreateWalletTest()
        {
            var wallet = new Wallet("1234", Environment.CurrentDirectory);

            Assert.True(wallet.IsCreated);
        }
    }
}