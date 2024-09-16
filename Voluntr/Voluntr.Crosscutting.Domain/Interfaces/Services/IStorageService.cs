namespace Voluntr.Crosscutting.Domain.Interfaces.Services
{
    public interface IStorageService
    {
        Task<byte[]> DownloadFile(string container, string path, string fileName);
        Task<string> GetFileUrl(string container, string path, string fileName);
        Task<string> UploadFile(string container, string path, string fileName, byte[] file);
        Task<bool> ExistsFile(string container, string path, string fileName);
        Task<bool> ExistsFiles(string container, string path);
        Task RemoveFile(string container, string path, string fileName);
    }
}
