﻿namespace DmogWallet
{
    public class ChainInfo
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Chain { get; private set; }

        public ChainInfo(string name, string version, string chain)
        {
            Name = name;
            Version = version;
            Chain = chain;
        }
    }
}