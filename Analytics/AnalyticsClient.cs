using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Avalonia_test.Analytics
{
    public static class AnalyticsClient
    {
        private const string Url = "http://localhost:55315/";
        
        private static HttpClient HttpClient = new HttpClient();
        private static Dictionary<string, object> _sessionProperties = new Dictionary<string, object>();

        static AnalyticsClient()
        {
            HttpClient.DefaultRequestHeaders.Add("ApiKey", "LiveCenter-Test");
            
            _sessionProperties.Add("Login", "test");
        }
        
        public static async Task OpenSessionAsync()
        {
            try
            {
                var response = await HttpClient.PostAsync(
                    requestUri: new Uri(new Uri(Url), "api/analytics/opensession"),
                    content: new StringContent(JsonConvert.SerializeObject(new OpenSessionInfo()
                    {
                        ClientHash = "test",
                    }), Encoding.UTF8, "application/json")).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
            }
        }

        public static async Task UpdateSessionAsync(ICollection<LiveMatch> mathes)
        {
            try
            {
                var updateSessionInfo = new UpdateSessionInfo()
                {
                    ClientHash = "test",
                    Properties = JsonConvert.SerializeObject(_sessionProperties),
                    Matches = JsonConvert.SerializeObject(mathes),
                };

                var response = await HttpClient.PostAsync(
                        requestUri: new Uri(new Uri(Url), "api/analytics/updatesession"),
                        content: new StringContent(JsonConvert.SerializeObject(updateSessionInfo), Encoding.UTF8,
                            "application/json"))
                    .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
            }
        }
        
        public static async Task SendMatches(ICollection<LiveMatch> mathes)
        {
            try
            {
                var liveMatchesInfo = new LiveMatchesInfo()
                {
                    ClientHash = "test",
                    LiveMatches = mathes,
                };

                var response = await HttpClient.PostAsync(
                        requestUri: new Uri(new Uri(Url), "api/analytics/setlivematches"),
                        content: new StringContent(JsonConvert.SerializeObject(liveMatchesInfo), Encoding.UTF8, "application/json"))
                    .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
            }
        }
    }
    
    public sealed class LiveMatchesInfo
    {
        public string ClientHash { get; set; }
        public ICollection<LiveMatch> LiveMatches { get; set; }
    }
}