using CloudinaryDotNet.Actions;

namespace Racing_Club.Interfaces;

public interface IPhotosService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}