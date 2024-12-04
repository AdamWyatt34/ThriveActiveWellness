using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.MediaStorage;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Storage;

public class AzureStorageService(BlobServiceClient blobServiceClient) : IStorageService
{
    public Uri GeneratePreSignedUrlAsync(string fileName, int expirationInMinutes)
    {
        BlobContainerClient? containerClient = blobServiceClient.GetBlobContainerClient("exercise-media-temp");
        BlobClient? blobClient = containerClient.GetBlobClient(fileName);

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerClient.Name,
            BlobName = fileName,
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes)
        };

        sasBuilder.SetPermissions(BlobContainerSasPermissions.Write);

        Uri? sasUri = blobClient.GenerateSasUri(sasBuilder);

        return sasUri;
    }

    public Uri MoveAsync(string sourceFileName, string destinationFileName)
    {
        BlobContainerClient? sourceContainerClient = blobServiceClient.GetBlobContainerClient("exercise-media-temp");
        BlobClient? sourceBlobClient = sourceContainerClient.GetBlobClient(sourceFileName);

        BlobContainerClient? destinationContainerClient = blobServiceClient.GetBlobContainerClient("exercise-media");
        BlobClient? destinationBlobClient = destinationContainerClient.GetBlobClient(destinationFileName);

        destinationBlobClient.StartCopyFromUri(sourceBlobClient.Uri);

        sourceBlobClient.Delete();

        return destinationBlobClient.Uri;
    }
}
