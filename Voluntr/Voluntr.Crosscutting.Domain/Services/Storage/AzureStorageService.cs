using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Voluntr.Crosscutting.Domain.Interfaces.Services;

namespace Voluntr.Crosscutting.Domain.Services.Storage
{
    public class AzureStorageService(
        StorageConfig configuration
    ) : IStorageService
    {
        private BlobContainerClient GetContainer(string containerName)
        {
            BlobContainerClient container = new(configuration.ConnectionString, containerName);
            container.CreateIfNotExists();

            return container;
        }

        public async Task<byte[]> DownloadFile(string container, string path, string fileName)
        {
            BlockBlobClient blob = GetContainer(container).GetBlockBlobClient($"{path}{fileName}");

            using var ms = new MemoryStream();
            if (blob.ExistsAsync().Result)
                await blob.DownloadToAsync(ms);

            return ms.ToArray();
        }

        public async Task<string> GetFileUrl(string container, string path, string fileName)
        {
            BlockBlobClient blob = await Task.Run(() => GetContainer(container).GetBlockBlobClient($"{path}{fileName}"));

            return blob.Uri.AbsoluteUri;
        }

        public async Task<string> UploadFile(string container, string path, string fileName, byte[] file)
        {
            BlockBlobClient blob = GetContainer(container).GetBlockBlobClient($@"{path}{fileName}");

            var streamFile = new MemoryStream(file);

            await blob.UploadAsync(streamFile);

            return @blob.Uri.AbsoluteUri;
        }

        public async Task<bool> ExistsFile(string container, string path, string fileName)
        {
            BlockBlobClient blob = GetContainer(container).GetBlockBlobClient(@$"{path}{fileName}");

            return await blob.ExistsAsync();
        }

        public async Task<bool> ExistsFiles(string container, string path)
        {
            if (!path.EndsWith('/'))
            {
                path += "/";
            }

            var blobContainerClient = GetContainer(container);

            await foreach (var _ in blobContainerClient.GetBlobsAsync(prefix: path))
            {
                return true;
            }

            return false;
        }


        public async Task RemoveFile(string container, string path, string fileName)
        {
            BlockBlobClient blob = GetContainer(container).GetBlockBlobClient(@$"{path}{fileName}");

            await blob.DeleteAsync();
        }
    }
}
