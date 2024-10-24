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
}
