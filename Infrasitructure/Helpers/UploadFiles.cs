using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasitructure.Helpers
{
    public static class UploadFiles
    {
        public static async Task<string> UploadFile(IFormFile file, string folderName, string wwwroot)
        {
            // unique name
            var extention = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + extention;
            // path
            var imagePath = Path.Combine(wwwroot, "Images/" + folderName);// ~/Images/Posts

            // check if folder exist
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            // full path
            var filePath = Path.Combine(imagePath, fileName);


            // upload file
            try
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                // log error
                Console.WriteLine(ex.Message);
                return "Uploading file error : " + ex.Message;

            }
            // return file name
            return "Images/" + folderName + "/" + fileName;
            // Images/Posts/bg65260hv245jkbh.png
        }
    }
}
