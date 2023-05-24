using System;
namespace PersonalBlog.Data.Services
{
	public interface IFirebaseService
	{
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}

