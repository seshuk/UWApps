using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProfileHelper
{
    public class TreeHouseUserProfile
    {
        public string Name { get; set; }
        public string ProfileName { get; set; }
        public string ProfileUrl { get; set; }
        public string GravatarUrl { get; set; }
        public int BadgeCout { get; set; }
        public int JavaScriptPoints { get; set; }
    }


    public class TreehouseProfileHelper
    {
        public async Task<TreeHouseUserProfile> GetUserProfile(string useName)
        {
            var userProfile = new TreeHouseUserProfile();


            HttpWebRequest request = WebRequest.Create("https://teamtreehouse.com/" + useName + ".json") as HttpWebRequest;
            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                { // this is an error

                    throw new Exception("Error reading data from server (" + response.StatusCode.ToString() + ")");
                }

                StreamReader reader = new StreamReader(response.GetResponseStream());

                dynamic profile = JsonConvert.DeserializeObject(reader.ReadToEnd());

                userProfile.Name = profile.name;
                userProfile.ProfileName = "@" + profile.profile_name;
                userProfile.GravatarUrl = profile.gravatar_url;
                userProfile.BadgeCout = profile.badges.Count;
                userProfile.JavaScriptPoints = profile.points.JavaScript;
                userProfile.ProfileUrl = profile.profile_url;
            }
            
            return userProfile;
        }
    }

}
