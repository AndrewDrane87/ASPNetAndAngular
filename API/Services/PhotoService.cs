using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API;

public class PhotoService
{
    private readonly Cloudinary cloudinary;
    public  PhotoService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
        cloudinary = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file,string objectType, string objectSubType, string publicId){
        var uploadResult = new ImageUploadResult();
        StringBuilder builder = new StringBuilder();
        builder.Append("mythos");
        if (objectType != null && objectType.Length > 0)
            builder.Append($"/{objectType}");
        if (objectSubType != null && objectSubType.Length > 0)
            builder.Append($"/{objectSubType}");

        Console.WriteLine(builder.ToString());

        if (file.Length>0){
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(400).Width(400).Crop("fill").Chain()
                .Quality("auto:good").Chain()
                .FetchFormat("auto").Chain(),
                PublicId = publicId,
                Folder = builder.ToString(),
            };
            uploadResult = await cloudinary.UploadAsync(uploadParams);
        }
        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        return await cloudinary.DestroyAsync(deleteParams);
    }
}
