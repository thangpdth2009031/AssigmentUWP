using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AssigmentPhamDucThangT2009M1.Service
{    
    public class FileService
    {

        private static string CloudName = "phamthanghehe";
        private static string CloudApiKey = "861299419565943";
        private static string CloudApiSecret = "2vmQ5P2jVeFQ7Dr43bQR2Z1zDvQ";

        static CloudinaryDotNet.Account account;
        static CloudinaryDotNet.Cloudinary cloudinary;
        public FileService()
        {
            if(account == null)
            {
                account = new CloudinaryDotNet.Account
                {
                    Cloud = CloudName,
                    ApiKey = CloudApiKey,
                    ApiSecret = CloudApiSecret
                };
            }
            if (cloudinary == null)
            {
                cloudinary = new CloudinaryDotNet.Cloudinary(account);
                cloudinary.Api.Secure = true;
            }
        }
        public async Task<string> UploadFile(StorageFile file)
        {
            if (file != null)
            {
                CloudinaryDotNet.Actions.RawUploadParams imageUploadParams = new CloudinaryDotNet.Actions.RawUploadParams
                {
                    File = new CloudinaryDotNet.FileDescription(file.Name, await file.OpenStreamForReadAsync())
                };
                //Thực hiện upload lấy thông tin về.
                CloudinaryDotNet.Actions.RawUploadResult result = await cloudinary.UploadAsync(imageUploadParams);
                return result.SecureUrl.OriginalString;
            }
            return null;
        }        
    }    
}
