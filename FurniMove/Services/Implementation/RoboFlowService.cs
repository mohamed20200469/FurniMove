using FurniMove.Services.Abstract;
using System.Net;
using System.Text.Json;
using System.Web;

namespace FurniMove.Services.Implementation
{
    public class RoboFlowService : IRoboFlowService
    {
        private readonly string _apiKey;
        private readonly string _modelEndpoint;

        public RoboFlowService(string apiKey, string modelEndpoint)
        {
            _apiKey = apiKey;
            _modelEndpoint = modelEndpoint;
        }

        public async Task<string> GetInferenceResultAsync(string imageUrl)
        {
            string uploadURL =
                "https://detect.roboflow.com/" + _modelEndpoint
                + "?api_key=" + _apiKey
                + "&image=" + System.Web.HttpUtility.UrlEncode(imageUrl);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(uploadURL, null);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Request failed with status code {response.StatusCode}");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement predictions = root.GetProperty("predictions");
                    if (predictions.GetArrayLength() > 0)
                    {
                        JsonElement firstPrediction = predictions[0];
                        string className = firstPrediction.GetProperty("class").GetString()!;
                        return className;
                    }
                    else
                    {
                        return "No predictions found.";
                    }
                }
            }
        }
    }
}
