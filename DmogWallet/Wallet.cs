using SubstrateNetApi;
using SubstrateNetApi.MetaDataModel.Values;
using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DmogWallet
{
    public class Wallet
    {
        private const string DefaultWalletFile = "wallet.dat";

        private readonly string _path;

        private WalletFile _walletFile;

        private Random _random = new Random();

        public bool IsUnlocked => Account != null;

        public bool IsCreated => _walletFile != null;

        public Account Account { get; private set; }

        public Wallet(string path = DefaultWalletFile)
        {
            _path = path;

            if (!Caching.TryReadFile(path, out _walletFile))
            {
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="password"></param>
        /// <param name="path"></param>
        public Wallet(string password, string path)
        {
            _path = path;

            if (!Caching.TryReadFile(path, out _walletFile))
            {
                Create(password);
            }
            else
            {
                Unlock(password);
            }
        }

        /// <summary>
        /// Create a new wallet with mnemonic
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Create(string password)
        {
            if (IsCreated)
            {
                return true;
            }

            byte[] randomBytes = new byte[48];

            _random.NextBytes(randomBytes);

            var memoryBytes = randomBytes.AsMemory();

            var pswBytes = Encoding.UTF8.GetBytes(password);

            var salt = memoryBytes.Slice(0, 16).ToArray();
            
            var seed = memoryBytes.Slice(16, 32).ToArray();

            pswBytes = SHA256.Create().ComputeHash(pswBytes);

            var encryptedSeed = ManagedAes.EncryptStringToBytes_Aes(Utils.Bytes2HexString(seed, Utils.HexStringFormat.PURE), pswBytes, salt);

            _walletFile = new WalletFile(encryptedSeed, salt);
           
            Caching.Persist(_path, _walletFile);

            Chaos.NaCl.Ed25519.KeyPairFromSeed(out byte[] publicKey, out byte[] privateKey, seed);

            Account = new Account(KeyType.ED25519, privateKey, publicKey);

            return true;
        }

        /// <summary>
        /// Unlock a locked wallet.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Unlock(string password)
        {
            if (IsUnlocked || !IsCreated)
            {
                return IsUnlocked && IsCreated;
            }

            var pswBytes = Encoding.UTF8.GetBytes(password);

            pswBytes = SHA256.Create().ComputeHash(pswBytes);

            var seed = ManagedAes.DecryptStringFromBytes_Aes(_walletFile.encryptedSeed, pswBytes, _walletFile.salt);

            Chaos.NaCl.Ed25519.KeyPairFromSeed(out byte[] publicKey, out byte[] privateKey, Utils.HexToByteArray(seed));

            Account = new Account(KeyType.ED25519, privateKey, publicKey);

            return true;
        }
    }
}
