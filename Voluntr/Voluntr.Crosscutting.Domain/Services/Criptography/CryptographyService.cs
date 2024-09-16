using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using System.Text;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.Security;

namespace Voluntr.Crosscutting.Domain.Services.Criptography
{
    public class CryptographyService(CryptographyConfig config) : ICryptographyService
    {
        public string Encrypt(string plain)
        {
            Encoding encoding = Encoding.ASCII;
            var pkcs = new Pkcs7Padding();
            IBlockCipherPadding padding = pkcs;

            var bcEngine = new BouncyCastleEngine(new AesEngine(), encoding);
            bcEngine.SetPadding(padding);

            return bcEngine.Encrypt(plain, config.Key);
        }

        public string Decrypt(string cipher)
        {
            Encoding encoding = Encoding.ASCII;
            var pkcs = new Pkcs7Padding();
            IBlockCipherPadding padding = pkcs;

            var bcEngine = new BouncyCastleEngine(new AesEngine(), encoding);
            bcEngine.SetPadding(padding);

            return bcEngine.Decrypt(cipher, config.Key);
        }
    }
}
