using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace PrivateRepoARMLinkedTemplateProxy
{
    public static class PrivateRepoARMLinkedTemplateProxy
    {
        [FunctionName("PrivateRepoARMLinkedTemplateProxy")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var finalResponse = (dynamic)null;
            var jsonReader = (dynamic)null;
            string privateRepoURI = req.Query["privateRepoURI"];
            string accessToken = req.Query["accessToken"];

            if (string.IsNullOrEmpty(privateRepoURI))
            {
                finalResponse = "Please pass a privateRepoURI value as a query string.";
            }
            else if (string.IsNullOrEmpty(accessToken))
            {
                finalResponse = "Please pass your private repo's accessToken value as a query string.";
            }
            else
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                privateRepoURI = privateRepoURI ?? data?.privateRepoURI;
                accessToken = accessToken ?? data?.accessToken;


                string strAuthHeader = "token " + accessToken;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3.raw");
                client.DefaultRequestHeaders.Add("Authorization", strAuthHeader);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(privateRepoURI);
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    var streamReader = new StreamReader(contentStream);
                    jsonReader = new JsonTextReader(streamReader);
                }
                catch (HttpRequestException exception)
                {
                    //TODO - handle exception
                    return new OkObjectResult("HttpException.");
                }

                JsonSerializer serializer = new JsonSerializer();
                try
                {
                    if (jsonReader != null)
                    {
                        finalResponse = serializer.Deserialize(jsonReader);
                    }

                }
                catch (JsonReaderException)
                {
                    return new OkObjectResult("Invalid JSON. The privateRepoURI must return valid JSON.");
                }
            }
            return new OkObjectResult(finalResponse);
        }
    }
}
