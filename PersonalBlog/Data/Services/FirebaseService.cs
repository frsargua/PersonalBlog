using System;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace PersonalBlog.Data.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly IConfiguration _configuration;

        public FirebaseService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var projectId = _configuration["Firebase:ProjectId"];
            var bucketName = _configuration["Firebase:BucketName"];

            var storage = await StorageClient.CreateAsync();
            var objectOptions = new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead };
            string fileName = Path.GetFileName(imageFile.FileName);
            string uniqueFileName = $"{bucketName}/{Path.GetFileNameWithoutExtension(fileName)}{Path.GetExtension(fileName)}";
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Upload the file to Firebase Storage
                await storage.UploadObjectAsync(bucketName, uniqueFileName, imageFile.ContentType, memoryStream, objectOptions);
            }

            var imageUrl = $"https://firebasestorage.googleapis.com/v0/b/{projectId}/o/{Uri.EscapeDataString(uniqueFileName)}?alt=media";

            return imageUrl;
        }
    }
}
