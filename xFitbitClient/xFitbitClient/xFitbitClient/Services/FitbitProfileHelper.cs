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
                    "Bearer eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE0NjU1MTgzMDYsInNjb3BlcyI6InJwcm8gcmFjdCIsInN1YiI6IjJZS1I5RiIsImF1ZCI6IjIyN000MyIsImlzcyI6IkZpdGJpdCIsInR5cCI6ImFjY2Vzc190b2tlbiIsImlhdCI6MTQ2NDkxMzUwNn0.5Q124HWMUwCk3guUoyLCyvz-3Xwma6eswWK7--Ou_ow");

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

