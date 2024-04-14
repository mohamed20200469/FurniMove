using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface IApplianceRepo
    {
        public Appliance? GetApplianceById(int applianceId);
        public ICollection<Appliance> GetAllAppliancesBy();
        public bool CreateAppliance(Appliance appliance);
        public bool UpdateAppliance(Appliance appliance);
        public bool DeleteAppliancebyId(int applianceId);
        bool Save();
    }
}
