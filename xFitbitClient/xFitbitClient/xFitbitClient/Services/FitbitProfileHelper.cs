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
            FitbitUserProfile userProfile = new FitbitUserProfile();


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
                    "Bearer eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIyWUtSOUYiLCJhdWQiOiIyMjdNNDMiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyYWN0IHJwcm8iLCJleHAiOjE0NzAyOTE1MDksImlhdCI6MTQ2NzY5OTUwOX0.16PLy_cR60YTlKg0Sr3yoscFpia-srSelA7DJUoOqng");

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

