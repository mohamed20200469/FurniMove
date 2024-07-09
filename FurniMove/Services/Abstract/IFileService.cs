namespace FurniMove.Services.Abstract
{
    public interface IFileService
    {
        public Task<Tuple<int, string>> SaveImage(IFormFile imageFile, string folder);
        public Task<bool> DeleteImage(string imageFileName, string folder);
    }
}
