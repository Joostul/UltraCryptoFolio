using System.Threading.Tasks;

namespace UltraCryptoFolio.Repositories
{
    public interface IAzureStorageAccountRepository
    {
        Task UploadTextAsync(string text, string containerName, string blobName);
        Task<string> DownloadTextAsync(string containerName, string blobName);
        Task<bool> BlobExistsAsync(string containerName, string blobName);
    }
}
