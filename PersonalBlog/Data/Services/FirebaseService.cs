using System;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

namespace PersonalBlog.Data.Services
{
    public class FirebaseService : IFirebaseService
    {

        public FirebaseService(IConfiguration configuration)
        {
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }



            var storage = StorageClient.Create();
            var objectOptions = new UploadObjectOptions() { PredefinedAcl = PredefinedObjectAcl.PublicRead };
            string fileName = Path.GetFileName(imageFile.FileName);
            string uniqueFileName = "personalblog-4f98f.appspot.com/" + Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName);
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);

                // Set the stream position back to 0 before the upload
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Upload the file to Firebase
                await storage.UploadObjectAsync("personalblog-4f98f.appspot.com", uniqueFileName, imageFile.ContentType, memoryStream, objectOptions);
            }

            var imageUrl = $"https://firebasestorage.googleapis.com/v0/b/personalblog-4f98f.appspot.com/o/{Uri.EscapeDataString(uniqueFileName)}?alt=media";

            return imageUrl;
        }
    }
}
