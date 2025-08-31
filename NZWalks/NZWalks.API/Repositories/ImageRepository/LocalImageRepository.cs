using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.ImageRepository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext nZWalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, NZWalksDbContext nZWalksDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZWalksDbContext = nZWalksDbContext;
        }
        
        public async Task<Image> Upload(Image image)
        {
            var fileNameWithExtension = image.FileName + image.FileExtension;

            var localFilePath = Path.Combine(
                webHostEnvironment.ContentRootPath,
                "Images",
                fileNameWithExtension);

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:{portnumber}/Images/filename.extension

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                              $"{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}" +
                              $"/Images/{image.FileName}{image.FileExtension}";
            
            image.Filepath = urlFilePath;

            await nZWalksDbContext.Images.AddAsync(image);
            await nZWalksDbContext.SaveChangesAsync();

            return image;
        }

    }
}
