using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Racing_Club.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloundinary;

    public PhotoService(IOptions<CloudinarySettings> config) // Will allow us to use the Cloudinary account
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);

        _cloundinary = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream), // Basic description 
                Transformation = new Transformation().Height(500).Width(500)
                    .Crop("fill").Gravity("face") // Basic image edition
            };

            uploadResult = await _cloundinary.UploadAsync(uploadParams);
        }

        // Everything working, will upload the picture
        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloundinary.DestroyAsync(deleteParams);

        return result;
    }
}