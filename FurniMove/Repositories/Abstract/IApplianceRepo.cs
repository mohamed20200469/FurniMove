using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface IApplianceRepo
    {
        public Task<Appliance?> GetApplianceById(int applianceId);
        public Task<List<Appliance>> GetAllAppliancesBy();
        public Task<bool> CreateAppliance(Appliance appliance);
        public Task<bool> UpdateAppliance(Appliance appliance);
        public Task<bool> DeleteAppliancebyId(int applianceId);
        public Task<bool> Save();
        Task<bool> AddTagsToAppliance(int applianceId, List<string> tags);
        Task<List<Appliance>> GetAppliancesByMove(int moveId);
    }
}
