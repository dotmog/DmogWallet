namespace DmogWallet
{
    public class WalletFile
    {
        private byte[] encryptedSeed;
        private byte[] salt;

        public WalletFile(byte[] encryptedSeed, byte[] salt)
        {
            this.encryptedSeed = encryptedSeed;
            this.salt = salt;
        }
    }
}
