namespace DEPIMVC.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        #region InsertImage
        public async Task<bool> SaveImageInWroot(IHasImage hasImage, IFormFile ImageFile, string Location = "Images/")
        {

            if (ImageFile != null)
            {
                string folder = Location;
                folder += Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string serverfolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(serverfolder));
                    using (var fileStream = new FileStream(serverfolder, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }
                    hasImage.Image = folder;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region Update


        public async Task<bool> UpdateImageInWroot(string OldImagePath, IHasImage hasImage, IFormFile NewImage)
        {

            await DeleteImageInWroot(OldImagePath);
            await SaveImageInWroot(hasImage, NewImage);
            return true;
        }






        #endregion

        #region Delete
        //Delete Image
        public async Task<bool> DeleteImageInWroot(string Image)
        {

            if (Image != null)
            {
                if (!string.IsNullOrEmpty(Image))
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, Image);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        return true;

                    }
                    return false;
                }
                return false;
            }
            return false;
        }


        #endregion


    }
}
