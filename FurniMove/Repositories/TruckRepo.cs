using FurniMove.Data;
using FurniMove.Interfaces.IRepositories;
using FurniMove.Models;

namespace FurniMove.Repositories
{
    public class TruckRepo : ITruckRepo
    {
        private readonly AppDbContext _db;
        public TruckRepo(AppDbContext db)
        {
            _db = db;
        }
        public bool createTruck(Truck truck)
        {
            _db.Trucks.Add(truck);
            return Save();
        }

        public bool deleteTruckById(int truckId)
        {
            var truck = _db.Trucks.FirstOrDefault(x => x.Id == truckId);
            if (truck != null)
            {
                _db.Trucks.Remove(truck);
            }
            return Save();
        }

        public ICollection<Truck> getAllTrucks()
        {
            return _db.Trucks.ToList();
        }

        public Truck? getTruckById(int truckId)
        {
            var truck = _db.Trucks.FirstOrDefault(x =>x.Id == truckId);
            return truck;
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool updateTruck(Truck truck)
        {
            _db.Trucks.Update(truck);
            return Save();
        }
    }
}
