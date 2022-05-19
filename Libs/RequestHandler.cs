using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace ServicesAutomation
{
    class RequestHandler
    {
        private static string API2_PATH = "https://api2.ploomes.com/";
        public static HttpClient client;

        public static async Task<JArray> MakeAsyncRequest(string url, Method method, JObject json = null)
        {
            bool success = false;
            string response = string.Empty;
            string statusCode = "TooManyRequests";
            url = API2_PATH + url;

            while (success != true && statusCode == "TooManyRequests")
            {
                if (method == Method.GET)
                {
                    HttpResponseMessage message = client.GetAsync(url).Result;
                    statusCode = message.StatusCode.ToString();
                    if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                    {
                        response = message.Content.ReadAsStringAsync().Result;
                        success = true;
                        return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                    }
                    else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                    {
                        JObject erro = new JObject()
                        {
                            { "Erro", message.StatusCode.ToString() }
                        };
                        JArray responseArray = new JArray();
                        responseArray.Add(erro);
                    }
                    else
                    {
                        statusCode = "TooManyRequests";
                    }
                }
                else if (method == Method.POST)
                {
                    if (json != null)
                    {
                        HttpResponseMessage message = client.PostAsync(url, new StringContent(json.ToString())).Result;
                        statusCode = message.StatusCode.ToString();
                        if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                        {
                            response = message.Content.ReadAsStringAsync().Result;
                            success = true;
                            return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                        }
                        else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                        {
                            JObject erro = new JObject()
                            {
                                { "Erro", message.StatusCode.ToString() }
                            };
                            JArray responseArray = new JArray();
                            responseArray.Add(erro);
                        }

                    }
                    else
                    {
                        HttpResponseMessage message = client.PostAsync(url, new StringContent(new JObject().ToString())).Result;
                        statusCode = message.StatusCode.ToString();
                        if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                        {
                            response = message.Content.ReadAsStringAsync().Result;
                            success = true;
                            return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                        }
                        else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                        {
                            JObject erro = new JObject()
                            {
                                { "Erro", message.StatusCode.ToString() }
                            };
                            JArray responseArray = new JArray();
                            responseArray.Add(erro);
                        }
                    }

                }
                else if (method == Method.DELETE)
                {
                    await client.DeleteAsync(url);
                    success = true;
                    JArray nullable = new JArray();
                    return nullable;// JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                }
                else if (method == Method.PATCH)
                {
                    var content = new ObjectContent<JObject>(json, new JsonMediaTypeFormatter());
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = content };
                    HttpResponseMessage message = client.SendAsync(request).Result;
                    statusCode = message.StatusCode.ToString();

                    if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                    {
                        response = message.Content.ReadAsStringAsync().Result;
                        success = true;
                        return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                    }
                    else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                    {
                        JObject erro = new JObject()
                        {
                            { "Erro", message.StatusCode.ToString() }
                        };
                        JArray responseArray = new JArray();
                        responseArray.Add(erro);
                    }
                }
            }
            return null;
        }
        public static JArray MakeRequest(string url, Method method, JObject json = null)
        {
            bool success = false;
            string response = string.Empty;
            string statusCode = "TooManyRequests";
            url = API2_PATH + url;

            while (success != true && statusCode == "TooManyRequests")
            {
                if (method == Method.GET)
                {
                    HttpResponseMessage message = client.GetAsync(url).Result;
                    statusCode = message.StatusCode.ToString();
                    if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                    {
                        response = message.Content.ReadAsStringAsync().Result;
                        success = true;
                        return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                    }
                    else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                    {
                        JObject erro = new JObject()
                        {
                            { "Erro", message.StatusCode.ToString() }
                        };
                        JArray responseArray = new JArray();
                        responseArray.Add(erro);
                    }
                    else
                    {
                        statusCode = "TooManyRequests";
                    }
                }
                else if (method == Method.POST)
                {
                    if (json != null)
                    {
                        HttpResponseMessage message = client.PostAsync(url, new StringContent(json.ToString())).Result;
                        statusCode = message.StatusCode.ToString();
                        if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                        {
                            response = message.Content.ReadAsStringAsync().Result;
                            success = true;
                            return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                        }
                        else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                        {
                            JObject erro = new JObject()
                            {
                                { "Erro", message.StatusCode.ToString() }
                            };
                            JArray responseArray = new JArray();
                            responseArray.Add(erro);
                        }

                    }
                    else
                    {
                        HttpResponseMessage message = client.PostAsync(url, new StringContent(new JObject().ToString())).Result;
                        statusCode = message.StatusCode.ToString();
                        if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                        {
                            response = message.Content.ReadAsStringAsync().Result;
                            success = true;
                            return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                        }
                        else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                        {
                            JObject erro = new JObject()
                            {
                                { "Erro", message.StatusCode.ToString() }
                            };
                            JArray responseArray = new JArray();
                            responseArray.Add(erro);
                        }
                    }

                }
                else if (method == Method.DELETE)
                {
                    client.DeleteAsync(url);
                    success = true;
                    JArray nullable = new JArray();
                    return nullable;// JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                }
                else if (method == Method.PATCH)
                {
                    var content = new ObjectContent<JObject>(json, new JsonMediaTypeFormatter());
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = content };
                    HttpResponseMessage message = client.SendAsync(request).Result;
                    statusCode = message.StatusCode.ToString();

                    if (message.StatusCode == System.Net.HttpStatusCode.OK || (message.StatusCode == System.Net.HttpStatusCode.Created || message.StatusCode == System.Net.HttpStatusCode.Accepted))
                    {
                        response = message.Content.ReadAsStringAsync().Result;
                        success = true;
                        return JsonConvert.DeserializeObject<JObject>(response)["value"] as JArray;
                    }
                    else if (message.StatusCode != System.Net.HttpStatusCode.TooManyRequests)
                    {
                        JObject erro = new JObject()
                        {
                            { "Erro", message.StatusCode.ToString() }
                        };
                        JArray responseArray = new JArray();
                        responseArray.Add(erro);
                    }
                }
            }
            return null;
        }

    }

    public enum Method
    {
        GET,
        POST,
        PATCH,
        DELETE
    }
}