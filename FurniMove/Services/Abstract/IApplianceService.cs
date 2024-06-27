using FurniMove.DTOs;

namespace FurniMove.Services.Abstract
{
    public interface IApplianceService
    {
        Task<bool> AddTagsToAppliance(int applianceId, List<string> tags);
        Task<ApplianceReadDTO> CreateAppliance(ApplianceWriteDTO dto, string uri);
        Task<List<ApplianceReadDTO>> GetAllAppliancesByMove(int moveId);
    }
}
