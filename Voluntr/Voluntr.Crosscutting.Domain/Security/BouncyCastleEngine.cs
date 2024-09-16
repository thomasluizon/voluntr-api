using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;

namespace Voluntr.Crosscutting.Domain.Security
{
    public class BouncyCastleEngine(
        IBlockCipher blockCipher,
        Encoding encoding
    )
    {
        private readonly IBufferedCipher cipher = CipherUtilities.GetCipher(blockCipher.AlgorithmName);
        private IBlockCipherPadding padding;

        public void SetPadding(IBlockCipherPadding padding)
        {
            this.padding = padding;
        }

        public string Encrypt(string plain, string key)
        {
            byte[] result = BouncyCastleCrypto(true, encoding.GetBytes(plain), key);
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string cipher, string key)
        {
            byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(cipher), key);
            return encoding.GetString(result);
        }

        private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, string key)
        {
            try
            {
                byte[] keyByte = encoding.GetBytes(key);
                KeyParameter keyParameter = new(keyByte);

                if (padding == null)
                    cipher.Init(forEncrypt, keyParameter);
                else
                    cipher.Init(forEncrypt, new ParametersWithRandom(keyParameter));

                return cipher.DoFinal(input);
            }
            catch (CryptoException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }
    }
}
