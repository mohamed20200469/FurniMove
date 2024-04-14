using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface ITruckRepo
    {
        public Truck? getTruckById(int truckId);
        public bool updateTruck(Truck truck);
        public bool deleteTruckById(int truckId);
        public bool createTruck(Truck truck);
        public ICollection<Truck> getAllTrucks();
        bool Save();
    }
}
