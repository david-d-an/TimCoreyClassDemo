using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HttpConsole {

class Program {
    static readonly HttpClient client = new HttpClient();
    
    static async Task Main(string[] args) {

        var sessionToken = await GetSessionToken();

        var url = 
            // "https://api.virbela.com/parse/classes/Room";
            // "https://parse-virbela-intern.herokuapp.com/parse/classes/Room";
            "https://parse-virbela-intern.herokuapp.com/parse";

        var userId = "eY11D3m8Qq";
        var accountId = "Z2oIUVW3Vy";
        var applicationId = "zquCagFDNXC8ipCFPjRjV8xw7y4Jik";

        dynamic respBodyRoom;
        dynamic respBodyCustomMenuItem;

        try	{
            string endPoint;
            
            endPoint = url + "/classes/Room";
            using (HttpRequestMessage msg = new(HttpMethod.Post, endPoint)) {
                // Headers
                // msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", your_token);
                // msg.Headers.Add("X-Parse-Application-Id", "zquCagFDNXC8ipCFPjRjV8xw7y4Jik");
                // msg.Headers.Add("X-Parse-Master-Key", "YDCoTqGx27qCQyWrY");

                // Body
                msg.Content = JsonContent.Create(new { 
                    where = new {
                        account = new {
                            __type = "Pointer",
                            className = "Account",
                            objectId = accountId
                        }
                    },
                    limit = 2000,
                    order = "-order",
                    _method = "GET",
                    _ApplicationId = applicationId,
                    _SessionToken = sessionToken
                });

                HttpResponseMessage response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                respBodyRoom = JsonConvert.DeserializeObject<dynamic>(
                    await response.Content.ReadAsStringAsync()
                );

                // foreach(var r in respBodyRoom.results) {
                //     var objectId = r.objectId?.Value;
                //     var name = r.name?.Value;
                //     var group = r.group?.Value;
                //     var accountObjectId = r.account?.objectId.Value;
                // }
                
                Console.WriteLine(respBodyRoom.results);
            }

            endPoint = url + "/classes/CustomMenuItem";
            using (HttpRequestMessage msg = new(HttpMethod.Post, endPoint)) {
                // Headers
                // msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", your_token);
                // msg.Headers.Add("X-Parse-Application-Id", "zquCagFDNXC8ipCFPjRjV8xw7y4Jik");
                // msg.Headers.Add("X-Parse-Master-Key", "YDCoTqGx27qCQyWrY");

                // Body
                msg.Content = JsonContent.Create(new { 
                    where = new {
                        account = new {
                            __type = "Pointer",
                            className = "Account",
                            objectId = accountId
                        }
                    },
                    limit = 1000,
                    order = "-order",
                    _method = "GET",
                    _ApplicationId = applicationId,
                    _SessionToken = sessionToken
                });

                HttpResponseMessage response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                respBodyCustomMenuItem = JsonConvert.DeserializeObject<dynamic>(
                    await response.Content.ReadAsStringAsync()
                );

                // foreach(var r in respBodyMenuItem.results) {
                //     var objectId = r.objectId?.Value;
                //     var name = r.name?.Value;
                //     var group = r.group?.Value;
                //     var accountObjectId = r.account?.objectId.Value;
                // }

                Console.WriteLine(respBodyCustomMenuItem.results);
            }

            var rooms = (JArray)(respBodyRoom?.results);
            var ja2 = (JArray)(respBodyCustomMenuItem?.results);
            rooms.Merge(respBodyCustomMenuItem?.results);

            foreach(var r in rooms) {
                var x = ((JValue)r["objectId"]).Value;
            }

            endPoint = url + "/classes/Membership";
            using (HttpRequestMessage msg = new(HttpMethod.Post, endPoint)) {
                // Headers
                // msg.Headers.Add("X-Parse-Application-Id", "zquCagFDNXC8ipCFPjRjV8xw7y4Jik");
                // msg.Headers.Add("X-Parse-Master-Key", "YDCoTqGx27qCQyWrY");

                // Body
                msg.Content = JsonContent.Create(new { 
                    where = new {
                        user = new {
                            __type = "Pointer",
                            className = "_User",
                            objectId = userId
                        },
                        account = new {
                            __type = "Pointer",
                            className = "Account",
                            objectId = accountId
                        }
                    },
                    limit = 1,
                    _method = "GET",
                    _ApplicationId = applicationId,
                    _SessionToken = sessionToken
                });

                HttpResponseMessage response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic respBody = JsonConvert.DeserializeObject<dynamic>(
                    await response.Content.ReadAsStringAsync()
                );
                var permission = respBody.results[0].permission.Value;

                // Console.WriteLine($"Permission: {permission}");
            }
        }
        catch(HttpRequestException e) {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
        }
    }

    private static async Task<string> GetSessionToken() {
        var url =  "https://parse-virbela-intern.herokuapp.com/parse";
        var accountId = "Z2oIUVW3Vy";
        var applicationId = "zquCagFDNXC8ipCFPjRjV8xw7y4Jik";
        string endPoint;

        try	{
            endPoint = url + "/functions/guiLogIn";
            using (HttpRequestMessage msg = new(HttpMethod.Post, endPoint)) {
                // Headers
                // msg.Headers.Add("X-Parse-Application-Id", "zquCagFDNXC8ipCFPjRjV8xw7y4Jik");

                // Body
                msg.Content = JsonContent.Create(new { 
                    username = "david.an@virbela.com",
                    password = "*A#JrY*M$z*2@y",
                    accountId = accountId,
                    origConfig = "NewDemoTest",
                    _ApplicationId = applicationId
                });

                HttpResponseMessage response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();

                // string responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseBody = JsonConvert.DeserializeObject<dynamic>(
                    await response.Content.ReadAsStringAsync()
                );
                var sessionToken = responseBody.result.sessionToken.Value;
                return  sessionToken;
            }
        }
        catch(HttpRequestException e) {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
            throw e;
        }
    }
}
}