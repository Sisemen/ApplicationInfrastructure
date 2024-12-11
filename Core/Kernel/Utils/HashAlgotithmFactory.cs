using System;
using System.Security.Authentication;
using System.Security.Cryptography;

namespace Core.Kernel.Utils
{
    public class HashAlgorithmFactory
    {
        public static HashAlgorithm Create(HashAlgorithmType type)
        {
            switch (type)
            {
                case HashAlgorithmType.Md5:
                    return MD5.Create();
                case HashAlgorithmType.Sha1:
                    return SHA1.Create();
                case HashAlgorithmType.Sha256:
                    return SHA256.Create();
                case HashAlgorithmType.Sha384:
                    return SHA384.Create();
                case HashAlgorithmType.Sha512:
                    return SHA512.Create();
                default:
                    throw new ArgumentException("Unsupported hash algorithm type.");
            }
        }
    }
}
