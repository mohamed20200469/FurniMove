using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IApplianceService
    {
        Task<bool> AddTagsToAppliance(int applianceId, List<string> tags);
        Task<ApplianceReadDTO> CreateAppliance(IFormFile img, int moveId, string uri);
        Task<List<ApplianceReadDTO>> GetAllAppliancesByMove(int moveId);
        Task<Appliance?> GetAppliance(int id);
        Task<List<string>> GetTagsAsync(Appliance appliance);
    }
}
