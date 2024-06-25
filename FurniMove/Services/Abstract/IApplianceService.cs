using FurniMove.DTOs;

namespace FurniMove.Services.Abstract
{
    public interface IApplianceService
    {
        Task<ApplianceReadDTO> CreateAppliance(ApplianceWriteDTO dto, string uri);
    }
}
