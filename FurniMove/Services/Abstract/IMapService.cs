namespace FurniMove.Services.Abstract
{
    public interface IMapService
    {
        public Task<(double DistanceInKm, double DurationInMinutes)> GetDistanceAndEta(double lat1, double lon1, double lat2, double lon2);
        public Task<string> GetAddress(double latitude, double longitude);
    }
}
