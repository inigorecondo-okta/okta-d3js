using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OktaWebApi
{
    public class User
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "created")]
        public string Created { get; set; }

        [JsonProperty(PropertyName = "activated")]
        public string Activated { get; set; }

        [JsonProperty(PropertyName = "statusChanged")]
        public string StatusChanged { get; set; }

        [JsonProperty(PropertyName = "lastLogin")]
        public string LastLogin { get; set; }

        [JsonProperty(PropertyName = "lastUpdated")]
        public string LastUpdated { get; set; }

        [JsonProperty(PropertyName = "passwordChanged")]
        public string PasswordChanged { get; set; }

        public string getAll(string endpoint, string method, string apitoken)
        {
            var responseText = "";
            var webRequest = System.Net.WebRequest.Create(new Uri(endpoint)) as System.Net.HttpWebRequest;

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

        public string deleteUser(string endPoint, string method, string apitoken)
        {
            var responseText = "";
            var webRequest = System.Net.WebRequest.Create(new System.Uri(endPoint)) as System.Net.HttpWebRequest;

            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.Headers.Add("Authorization", "SSWS " + apitoken);
                webRequest.Accept = "application/json";
                webRequest.ContentType = "application/json";

                try
                {
                    var response = webRequest.GetResponse();
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                catch (System.Net.WebException ex)
                {
                    using (System.Net.WebResponse response = ex.Response)
                    {
                        var httpResponse = (System.Net.HttpWebResponse)response;

                        using (System.IO.Stream data = response.GetResponseStream())
                        {
                            try
                            {
                                System.IO.StreamReader sr = new System.IO.StreamReader(data);
                                throw new System.Exception(sr.ReadToEnd());
                            }
                            catch (System.Exception e1)
                            {
                                responseText = "Error: " + e1.ToString();
                            }
                        }
                    }
                }
            }

            return responseText;
        }


    }

}