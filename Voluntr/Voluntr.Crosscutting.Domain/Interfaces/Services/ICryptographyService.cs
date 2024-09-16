namespace Voluntr.Crosscutting.Domain.Interfaces.Services
{
    public interface ICryptographyService
    {
        public string Encrypt(string plain);
        public string Decrypt(string cipher);
    }
}
