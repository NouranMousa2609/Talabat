namespace LinkDev.Talabat.Dashboard.Helpers
{
    public class PictureSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);


            var fileName = Guid.NewGuid() + file.FileName;


            var filePath = Path.Combine(folderPath, fileName);


            var fs = new FileStream(filePath, FileMode.Create);


            file.CopyTo(fs);

            return Path.Combine("images\\products", fileName);
        }
        public static void DeleteFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}