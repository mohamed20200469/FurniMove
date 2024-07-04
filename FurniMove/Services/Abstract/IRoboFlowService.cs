
namespace FurniMove.Services.Abstract
{
    public interface IRoboFlowService
    {
        Task<string> GetInferenceResultAsync(string imageUrl);
    }
}
