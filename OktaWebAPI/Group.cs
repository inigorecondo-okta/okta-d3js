using Newtonsoft.Json;

namespace OktaWebAPI
{

    public class Group
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "users")]
        public string Users { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }


        public string getGroups(string endpoint, string method, string apitoken)
        {
            var responseText = "";
            var webRequest = System.Net.WebRequest.Create(new System.Uri(endpoint)) as System.Net.HttpWebRequest;

            try
            {
                if (webRequest != null)
                {
                    webRequest.Method = method;
                    webRequest.Headers.Add("Authorization", "SSWS " + apitoken);
                    webRequest.Accept = "application/json";
                    webRequest.ContentType = "application/json";

                    var response = webRequest.GetResponse();
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                responseText = "Error: " + ex.ToString();
            }

            return responseText;
        }

    }
}
