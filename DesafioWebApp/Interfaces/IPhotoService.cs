using CloudinaryDotNet.Actions;

namespace DesafioWebApp.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

    Task<DeletionResult> DeletePhotoAsync(string publicUrl);
}