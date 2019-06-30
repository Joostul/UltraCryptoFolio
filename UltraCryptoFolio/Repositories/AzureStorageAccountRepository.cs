using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Repositories
{
    public class AzureStorageAccountRepository : IAzureStorageAccountRepository
    {
        private readonly CloudStorageAccount _storageAccount;

        public AzureStorageAccountRepository(string storageAccountConnectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(storageAccountConnectionString);
        }

        private async Task<CloudBlobContainer> GetBlobContainerAsync(string containerName)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            return container;
        }

        private async Task<CloudBlockBlob>  GetCloudBlockBlobAsync(string containerName, string blobName)
        {
            var container = await GetBlobContainerAsync(containerName);
            return container.GetBlockBlobReference(blobName);
        }

        public async Task UploadTextAsync(string text, string containerName, string blobName)
        {
            var blob = await GetCloudBlockBlobAsync(containerName, blobName);
            await blob.UploadTextAsync(text);
        }

        public async Task<string> DownloadTextAsync(string containerName, string blobName)
        {
            var blob = await GetCloudBlockBlobAsync(containerName, blobName);
            if(await blob.ExistsAsync())
            {
                return await blob.DownloadTextAsync();
            } else
            {
                return null;
            }
        }

        public async Task<bool> BlobExistsAsync(string containerName, string blobName)
        {
            var blob = await GetCloudBlockBlobAsync(containerName, blobName);
            return await blob.ExistsAsync();
        }

        public async Task MoveBlobAsync(string originContainerName, string originBlobName, string destinationContainerName, string destinationBlobName)
        {
            if(await BlobExistsAsync(originContainerName, originBlobName))
            {
                var originBlob = await GetCloudBlockBlobAsync(originContainerName, originBlobName);
                var destinationBlob = await GetCloudBlockBlobAsync(destinationContainerName, destinationBlobName);
                await destinationBlob.StartCopyAsync(originBlob);
            }
        }
    }
}
