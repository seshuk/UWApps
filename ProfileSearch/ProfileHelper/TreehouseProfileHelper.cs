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
        public string Profile_Name { get; set; }
        public string Profile_Url { get; set; }
        public string Gravatar_Url { get; set; }
        public Badge[] Badges { get; set; }
        public Points Points { get; set; }
    }

    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public DateTime EarnedDate { get; set; }
        public Course[] Courses { get; set; }
    }

    public class Course
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int BadgeCout { get; set; }
        
    }

    public class Points
    {
        public int Total { get; set; }
        public int HTML { get; set; }
        public int CSS { get; set; }
        public int Design { get; set; }
        public int JavaScript { get; set; }
        public int Ruby { get; set; }
        public int PHP { get; set; }
        public int WordPress { get; set; }
        public int iOS { get; set; }
        public int Android { get; set; }
        public int Development_Tools { get; set; }
        public int Business { get; set; }
        public int Python { get; set; }
        public int Java { get; set; }
        public int Digital_Literacy { get; set; }
        public int Game_Development { get; set; }
        public int CSharp { get; set; }
        public int Database { get; set; }
        
    }


    public class TreehouseProfileHelper
    {
        public async Task<TreeHouseUserProfile> GetUserProfile(string useName)
        {
            TreeHouseUserProfile userProfile = null;// new TreeHouseUserProfile();


            HttpWebRequest request = WebRequest.Create("https://teamtreehouse.com/" + useName + ".json") as HttpWebRequest;
            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                { // this is an error

                    throw new Exception("Error reading data from server (" + response.StatusCode.ToString() + ")");
                }

                StreamReader reader = new StreamReader(response.GetResponseStream());
                
                //dynamic profile = JsonConvert.DeserializeObject(reader.ReadToEnd());
                var profile = JsonConvert.DeserializeObject<TreeHouseUserProfile>(reader.ReadToEnd());
                userProfile = profile;

                //INV: For some reason the Dynamic JSON deserialization not working.

               /* userProfile.Name = profile.Name; //profile.name;
                userProfile.ProfileName = "@" + profile.ProfileName ;// profile.profile_name;
                userProfile.GravatarUrl = profile.GravatarUrl ;// gravatar_url;
                userProfile.BadgeCout = profile.Badges.Length;// badges.Count;
                userProfile.JavaScriptPoints = profile.Points.JavaScript ;// points.JavaScript;
                userProfile.ProfileUrl = profile.profile_url;*/
            }
            
            return userProfile;
        }
    }

}
