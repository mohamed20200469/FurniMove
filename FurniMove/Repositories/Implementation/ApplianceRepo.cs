using FurniMove.Data;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FurniMove.Repositories.Implementation
{
    public class ApplianceRepo : IApplianceRepo
    {
        private readonly AppDbContext _db;
        public ApplianceRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAppliance(Appliance appliance)
        {
            await _db.Appliances.AddAsync(appliance);
            return await Save();
        }

        public async Task<bool> DeleteAppliancebyId(int applianceId)
        {
            var appliance = await _db.Appliances.FirstOrDefaultAsync(p => p.Id == applianceId);
            if (appliance == null)
            {
                return false;
            }
            _db.Appliances.Remove(appliance);
            return await Save();
        }

        public async Task<List<Appliance>> GetAllAppliancesBy()
        {
            return await _db.Appliances.ToListAsync();
        }

        public async Task<Appliance?> GetApplianceById(int applianceId)
        {
            var appliance = await _db.Appliances.FirstOrDefaultAsync(_ => _.Id == applianceId);
            return appliance;
        }

        public async Task<bool> Save()
        {
            var saved = await _db.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateAppliance(Appliance appliance)
        {
            _db.Appliances.Update(appliance);
            return await Save();
        }
        public async Task<bool> AddTagsToAppliance(int applianceId, List<string> tags)
        {
            var appliance = await _db.Appliances.FindAsync(applianceId);
            if (appliance == null) { return false; }

            appliance.Tags.AddRange(tags);
            appliance.Tags = appliance.Tags.Distinct().ToList();

            return await Save();
        }

        public async Task<List<Appliance>> GetAppliancesByMove(int moveId)
        {
            var appliances = await _db.Appliances.Where(x => x.moveRequestId == moveId).ToListAsync();
            return appliances;
        }
    }
}
