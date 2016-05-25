using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using xFitbitClient.Models;

namespace xFitbitClient
{

    public class FitbitProfileHelper
    {
        public async Task<FitbitUserProfile> GetUserProfile()
        {
            FitbitUserProfile userProfile = null;


            try
            {
            var uri = new Uri("https://api.fitbit.com/1/user/-/profile.json");

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };

            request.Headers.Add("Authorization",
                "Bearer eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE0NjQ1Nzk1ODcsInNjb3BlcyI6InJwcm8gcmxvYyByYWN0Iiwic3ViIjoiMllLUjlGIiwiYXVkIjoiMjI3TTQzIiwiaXNzIjoiRml0Yml0IiwidHlwIjoiYWNjZXNzX3Rva2VuIiwiaWF0IjoxNDYzOTc0Nzg3fQ.UTdGNwLRHVq4rVWYckTwvFFgqz-ck-c3yXqr_LBmmfA");

            var response = await client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
                var profile = JsonConvert.DeserializeObject<FitbitUserProfile>(content);
                userProfile = profile;
               
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return userProfile;
        }
    }

}

