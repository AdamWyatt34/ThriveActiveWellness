namespace ThriveActiveWellness.Modules.Exercises.Application.Abstractions.MediaStorage;

public interface IStorageService
{
    Uri GeneratePreSignedUrlAsync(string fileName, int expirationInMinutes);
    Uri MoveAsync(string sourceFileName, string destinationFileName);
}
