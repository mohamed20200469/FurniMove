using FurniMove.Services.Abstract;
using Newtonsoft.Json.Linq;

namespace FurniMove.Services.Implementation
{
    public class MapService : IMapService
    {
        private readonly string _apiKey;

        public MapService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<(double DistanceInKm, double DurationInMinutes)> GetDistanceAndEta(double lat1, double lon1, double lat2, double lon2)
        {
            using (var client = new HttpClient())
            {
                var requestUrl = $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={lat1},{lon1}&destinations={lat2},{lon2}&key={_apiKey}";
                var response = await client.GetStringAsync(requestUrl);
                var json = JObject.Parse(response);

                var distanceInMeters = json["rows"][0]["elements"][0]["distance"]["value"].Value<double>();
                var durationInSeconds = json["rows"][0]["elements"][0]["duration"]["value"].Value<double>();

                var distanceInKm = distanceInMeters / 1000; // Convert meters to kilometers
                var durationInMinutes = durationInSeconds / 60; // Convert seconds to minutes

                return (distanceInKm, durationInMinutes);
            }
        }

        public async Task<string> GetAddress(double latitude, double longitude)
        {
            using var client = new HttpClient();
            var requestUrl = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={_apiKey}";
            var response = await client.GetStringAsync(requestUrl);
            var json = JObject.Parse(response);

            var formattedAddress = json["results"]![0]!["formatted_address"]!.Value<string>();

            // Split the address into parts using commas
            var addressParts = formattedAddress!.Split(',');

            if (addressParts.Length < 3)
            {
                return "Invalid address format";
            }

            // Get the parts you want
            var firstPart = addressParts[0].Trim();
            var secondPart = addressParts[2].Trim();
            var thirdPart = addressParts[3].Trim();

            // Construct the new address string
            var newAddress = $"{firstPart}, {secondPart}, {thirdPart}";
            return newAddress;
        }
    }
}
