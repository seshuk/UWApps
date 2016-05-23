using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace xFitbitClient
{

    public class Features
    {

        [JsonProperty("exerciseGoal")]
        public bool ExerciseGoal { get; set; }
    }

    public class TopBadge
    {

        [JsonProperty("badgeGradientEndColor")]
        public string BadgeGradientEndColor { get; set; }

        [JsonProperty("badgeGradientStartColor")]
        public string BadgeGradientStartColor { get; set; }

        [JsonProperty("badgeType")]
        public string BadgeType { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("cheers")]
        public object[] Cheers { get; set; }

        [JsonProperty("dateTime")]
        public string DateTime { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("earnedMessage")]
        public string EarnedMessage { get; set; }

        [JsonProperty("encodedId")]
        public string EncodedId { get; set; }

        [JsonProperty("image100px")]
        public string Image100px { get; set; }

        [JsonProperty("image125px")]
        public string Image125px { get; set; }

        [JsonProperty("image300px")]
        public string Image300px { get; set; }

        [JsonProperty("image50px")]
        public string Image50px { get; set; }

        [JsonProperty("image75px")]
        public string Image75px { get; set; }

        [JsonProperty("marketingDescription")]
        public string MarketingDescription { get; set; }

        [JsonProperty("mobileDescription")]
        public string MobileDescription { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shareImage640px")]
        public string ShareImage640px { get; set; }

        [JsonProperty("shareText")]
        public string ShareText { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("timesAchieved")]
        public int TimesAchieved { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class User
    {

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("avatar150")]
        public string Avatar150 { get; set; }

        [JsonProperty("averageDailySteps")]
        public int AverageDailySteps { get; set; }

        [JsonProperty("corporate")]
        public bool Corporate { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("distanceUnit")]
        public string DistanceUnit { get; set; }

        [JsonProperty("encodedId")]
        public string EncodedId { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("glucoseUnit")]
        public string GlucoseUnit { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("heightUnit")]
        public string HeightUnit { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("memberSince")]
        public string MemberSince { get; set; }

        [JsonProperty("offsetFromUTCMillis")]
        public int OffsetFromUTCMillis { get; set; }

        [JsonProperty("startDayOfWeek")]
        public string StartDayOfWeek { get; set; }

        [JsonProperty("strideLengthRunning")]
        public double StrideLengthRunning { get; set; }

        [JsonProperty("strideLengthRunningType")]
        public string StrideLengthRunningType { get; set; }

        [JsonProperty("strideLengthWalking")]
        public double StrideLengthWalking { get; set; }

        [JsonProperty("strideLengthWalkingType")]
        public string StrideLengthWalkingType { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("topBadges")]
        public TopBadge[] TopBadges { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("weightUnit")]
        public string WeightUnit { get; set; }
    }

    public class FitbitUserProfile
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }


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

