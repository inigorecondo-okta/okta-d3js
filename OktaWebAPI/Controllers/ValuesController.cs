using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using OktaWebApi;

namespace OktaWebAPI.Controllers
{
    public class ValuesController : ApiController
    {

        [HttpGet]
        [Route("getUsers")]
        public string getUsers()
        {
            System.Text.StringBuilder d3js = new System.Text.StringBuilder();
            System.Text.StringBuilder groups_d3js = new System.Text.StringBuilder();
            System.Text.StringBuilder apps_d3js = new System.Text.StringBuilder();

            User userItem = new User();
            string oktaEnv = System.Web.Configuration.WebConfigurationManager.AppSettings["okta:org"];
            string apiToken = System.Web.Configuration.WebConfigurationManager.AppSettings["okta:apitoken"];
            string endPoint = string.Concat(oktaEnv + "/api/v1/users?limit=10");
            string json = userItem.getAll(endPoint, "GET", apiToken);

            //Group groupItem = new Group();
            //string endpoint = string.Concat(oktaEnv + "/api/v1/groups?limit=100");
            //string jsonGroups = groupItem.getGroups(endpoint, "GET", apiToken);

            try
            {
                dynamic users = JsonConvert.DeserializeObject(json);

                d3js.AppendFormat("{{\"name\": \"{0}\", \"children\":" + "[", oktaEnv);

                int numUsers = 0;
                int numGroups = 0;
                int numApps = 0;
                foreach (var user in users)
                {

                    // Groups from each user

                    try
                    {
                        string endpoint_groups = oktaEnv + "/api/v1/users/" + user.id.Value + "/groups";
                        string json_groups = userItem.getAll(endpoint_groups, "GET", apiToken);

                        dynamic groups = JsonConvert.DeserializeObject(json_groups);
                        foreach (var group in groups)
                        {
                            groups_d3js.AppendFormat("{{\"name\": \"{0}\", \"children\":", group.profile.name.Value == null ? "" : group.profile.name.Value.ToString());
                            groups_d3js.AppendFormat("[");
                            groups_d3js.AppendFormat("{{\"name\": \"Name: {0}\", \"size\": 3938}},", group.profile.name.Value == null ? "" : group.profile.name.Value.ToString());
                            groups_d3js.AppendFormat("{{\"name\": \"Desc: {0}\", \"size\": 3938}}", group.profile.description.Value == null ? "" : group.profile.description.Value.ToString());
                            groups_d3js.AppendFormat("]");
                            groups_d3js.AppendFormat("}}");
                            if (numGroups < groups.Count - 1)
                            {
                                groups_d3js.AppendFormat(",");  // to every item except the last one.
                            }
                            numGroups++;
                        }

                        string endpoint_apps = oktaEnv + "/api/v1/users/" + user.id.Value + "/appLinks";
                        string json_apps = userItem.getAll(endpoint_apps, "GET", apiToken);

                        dynamic apps = JsonConvert.DeserializeObject(json_apps);
                        foreach (var app in apps)
                        {
                            apps_d3js.AppendFormat("{{\"name\": \"{0}\", \"children\":", app.label.Value == null ? "" : app.label.Value.ToString());
                            apps_d3js.AppendFormat("[");
                            apps_d3js.AppendFormat("{{\"name\": \"App Name: {0}\", \"size\": 3938}},", app.appName.Value == null ? "" : app.appName.Value.ToString());
                            apps_d3js.AppendFormat("{{\"name\": \"Link: {0}\", \"size\": 3938}}", app.linkUrl.Value == null ? "" : app.linkUrl.Value.ToString());
                            apps_d3js.AppendFormat("]");
                            apps_d3js.AppendFormat("}}");
                            if (numApps < apps.Count - 1)
                            {
                                apps_d3js.AppendFormat(",");  // to every item except the last one.
                            }
                            numApps++;
                        }

                    }
                    catch (Exception ex)
                    {
                    }

                    d3js.AppendFormat("{{\"name\": \"User: {0}\", \"children\":", user.profile.login.Value == null ? "" : user.profile.login.Value.ToString());
                    d3js.AppendFormat("[");
                    //d3js.AppendFormat("{{\"name\": \"parent: 00u2ojcqxtloZ9YL61t7\", \"size\": 3938}},");
                    d3js.AppendFormat("{{\"name\": \"First Name: {0}\", \"size\": 3938}},", user.profile.firstName.Value == null ? "" : user.profile.firstName.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Last Name: {0}\", \"size\": 3938}},", user.profile.lastName.Value == null ? "" : user.profile.lastName.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Email: {0}\", \"size\": 3938}},", user.profile.email.Value == null ? "" : user.profile.email.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Second Email: {0} \", \"size\": 3938}},", user.profile.secondEmail.Value == null ? "" : user.profile.secondEmail.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Mobile Phone: {0}\", \"size\": 3938}},", user.profile.mobilePhone.Value == null ? "" : user.profile.mobilePhone.Value.ToString());
                    //d3js.AppendFormat("{{\"name\": \"Login: {0}\", \"size\": 3938}},", user.profile.login.Value == null ? "" : user.profile.login.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Status: {0}\", \"size\": 3938}},", user.status.Value == null ? "" : user.status.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Type: {0}\", \"size\": 3938}},", user.credentials.provider.type.Value == null ? "" : user.credentials.provider.type.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Created: {0}\", \"size\": 3938}},", user.created.Value == null ? "" : user.created.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Activated: {0} \", \"size\": 3938}},", user.activated.Value == null ? "" : user.activated.Value.ToString());
                    //d3js.AppendFormat("{{\"name\": \"Status Changed: {0} \", \"size\": 3938}},", user.statusChanged.Value == null ? "" : user.statusChanged.Value.ToString());
                    //d3js.AppendFormat("{{\"name\": \"Last Login: {0} \", \"size\": 3938}},", user.lastLogin.Value == null ? "" : user.lastLogin.Value.ToString());
                    //d3js.AppendFormat("{{\"name\": \"Last Updated: {0} \", \"size\": 3938}},", user.lastUpdated.Value == null ? "" : user.lastUpdated.Value.ToString());
                    d3js.AppendFormat("{{\"name\": \"Password Changed: {0}\", \"size\": 3938}}", user.passwordChanged.Value == null ? "" : user.passwordChanged.Value.ToString());
                    if (numGroups > 0)
                    {
                        d3js.AppendFormat(",{{\"name\": \"Groups\", \"children\":");
                        d3js.AppendFormat("[");
                        d3js.Append(groups_d3js.ToString());
                        d3js.AppendFormat("]");
                        d3js.AppendFormat("}}");
                    }
                    if (numApps > 0)
                    {
                        d3js.AppendFormat(",{{\"name\": \"Apps\", \"children\":");
                        d3js.AppendFormat("[");
                        d3js.Append(apps_d3js.ToString());
                        d3js.AppendFormat("]");
                        d3js.AppendFormat("}}");
                    }
                    //d3js.AppendFormat("{{\"name\": \"credentials\", \"children\":");
                    //d3js.Append("[");
                    //d3js.AppendFormat("{{\"name\": \"provider\", \"children\":");
                    //d3js.Append("[");
                    //d3js.AppendFormat("{{\"name\": \"type: ACTIVE_DIRECTORY\", \"size\": 3938}},");
                    //d3js.AppendFormat("{{\"name\": \"awsdcirecondo.co.uk\", \"size\": 3938}}");
                    //d3js.AppendFormat("]");
                    //d3js.AppendFormat("}},");
                    //d3js.AppendFormat("{{\"name\": \"_links\", \"children\":");
                    //d3js.AppendFormat("[");
                    //d3js.AppendFormat("{{\"name\":\"self\", \"children\":");
                    //d3js.AppendFormat("[");
                    //d3js.AppendFormat("{{\"name\": \"href: https://dcirecondo.okta.com/api/v1/users/00u2ojcqxtloZ9YL61t7\", \"size\": 3938}}");
                    //d3js.AppendFormat("]");
                    //d3js.AppendFormat("}}");
                    //d3js.AppendFormat("]");
                    //d3js.AppendFormat("}}");
                    //d3js.AppendFormat("]");
                    //d3js.AppendFormat("}}");
                    d3js.AppendFormat("]");
                    d3js.AppendFormat("}}");

                    if (numUsers < users.Count - 1)
                    {
                        d3js.AppendFormat(",");  // to every item except the last one.
                    }
                    groups_d3js.Clear();
                    apps_d3js.Clear();
                    numUsers++;
                    numGroups = 0;
                    numApps = 0;
                }

                d3js.Append("]}");
            }
            catch (Exception ex)
            {
            }

            return d3js.ToString();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
