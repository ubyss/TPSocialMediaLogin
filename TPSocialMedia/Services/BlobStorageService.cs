using Azure.Storage.Blobs;

namespace TPSocialMedia.Services
{
    public class BlobStorageService
    {
        private readonly string connectionString = "<your_connection_string>";
        private readonly string containerName = "<your_container_name>";

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Upload the file
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(imageStream, overwrite: true);

            // Get the URL of the uploaded file
            string imageUrl = blobClient.Uri.AbsoluteUri;

            return imageUrl;
        }
    }
}
