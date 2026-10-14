namespace RaceDay_Part2.Models.Interface
{
    public interface IProfileImageService
    { 
        Task<string?> SaveAsync(IFormFile file);

        void Delete(string? relativePath);

    }
    public class ProfileImageService : IProfileImageService
    {
        private static readonly string[] Allowed = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSize = 2 * 1024 * 1024; // 2 MB
        private readonly IWebHostEnvironment _env;
        public ProfileImageService(IWebHostEnvironment env) => _env = env;
        public async Task<string?> SaveAsync(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!Allowed.Contains(ext) || file.Length > MaxFileSize)
            {
                return null;
            }
            var folder = Path.Combine(_env.WebRootPath, "uploads", "profile");
            Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folder, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            return $"/uploads/profile/{fileName}";
        }

        public void Delete(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            var filePath = Path.Combine(
                _env.WebRootPath,
                relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
