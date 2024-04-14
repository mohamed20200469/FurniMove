using FurniMove.Data;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;

namespace FurniMove.Repositories.Implementation
{
    public class ApplianceRepo : IApplianceRepo
    {
        private readonly AppDbContext _db;
        public ApplianceRepo(AppDbContext db)
        {
            _db = db;
        }

        public bool CreateAppliance(Appliance appliance)
        {
            _db.Appliances.Add(appliance);
            return Save();
        }

        public bool DeleteAppliancebyId(int applianceId)
        {
            var appliance = _db.Appliances.FirstOrDefault(p => p.Id == applianceId);
            if (appliance == null)
            {
                return false;
            }
            _db.Appliances.Remove(appliance);
            return Save();
        }

        public ICollection<Appliance> GetAllAppliancesBy()
        {
            return _db.Appliances.ToList();
        }

        public Appliance? GetApplianceById(int applianceId)
        {
            var appliance = _db.Appliances.FirstOrDefault(_ => _.Id == applianceId);
            return appliance;
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAppliance(Appliance appliance)
        {
            _db.Update(appliance);
            return Save();
        }
    }
}
