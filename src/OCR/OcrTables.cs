using System.Net.Http.Headers;
using RestSharp;

public class OcrTables
{
    //lunch ocr tables job by passing url (using RestSharp)
    public static void LunchJob_RestSharp(string api_key)
    {
        var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest("ocr/ocr_tables_async", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("authorization", $"Bearer {api_key}");
        request.AddHeader("content-type", "application/json");
        request.AddParameter("application/json", "{\"providers\":\"providers_here\",\"language\":\"lang_here\",\"show_original_response\":false,\"file_url\":\"image_url_here\"}", ParameterType.RequestBody);
        var response = client.Execute(request);
        Console.WriteLine(response.Content);
    }
    //lunch ocr tables job by passing url (using HttpClient)
    public static void LunchJob_HttpClient(string api_key)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/ocr/ocr_tables_async"),
            Headers =
    {
        { "accept", "application/json" },
        { "authorization", $"Bearer {api_key}" },
    },
            Content = new StringContent("{\"show_original_response\":false,\"providers\":\"providers_here\",\"file_url\":\"image_url_here\"}")
            {
                Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
            }
        };
        using (var response = client.Send(request))
        {
            var body = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
        }

    }
    //lunch ocr tables job by passing local file path(using RestSharp)
    public static void LunchJob_RestSharp(string api_key, string file_path)
    {
        var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest("ocr/ocr_tables_async", Method.Post);


        request.AddHeader("Authorization", $"Bearer {api_key}");

        request.AddParameter("providers", "microsoft");
        request.AddParameter("language", "en");

        byte[] fileBytes = File.ReadAllBytes(file_path);
        request.AddFile("file", fileBytes, Path.GetFileName(file_path));

        var response = client.Execute(request);

        string responseContent = response.Content;
        Console.WriteLine(responseContent);
    }
    //lunch ocr tables job by passing local file path(using HttpClient)
    public static void LunchJob_HttpClient(string api_key, string file_path)
    {
        var client = new HttpClient();
        var content = new MultipartFormDataContent();

        content.Add(new StreamContent(File.OpenRead(file_path)), "file", Path.GetFileName(file_path));
        content.Add(new StringContent("microsoft"), "providers");
        content.Add(new StringContent("en"), "language");

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.edenai.run/v2/ocr/ocr_tables_async"));
        request.Content = content;
        request.Headers.Add("Authorization", $"Bearer {api_key}");
        using (var response = client.Send(request))
        {
            var body = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
        }
    }
    //get ocr tables job results (using RestSharp)
    public static void GetJob_RestSharp(string api_key, string id)
    {
        var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest($"ocr/ocr_tables_async/{id}", Method.Get);

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {api_key}");

        request.AddParameter("response_as_dict", true);
        request.AddParameter("show_original_response", true);
        var response = client.Execute(request);
        Console.WriteLine(response.Content);
    }
    //get ocr tables job results (using HttpClient)
    public static void GetJob_HttpClient(string api_key, string id)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.edenai.run/v2/ocr/ocr_tables_async/{id}"),
            Headers =
    {
        { "authorization", $"Bearer {api_key}" },
        { "accept", "application/json" },
    },
        };
        using (var response = client.Send(request))
        {
            var body = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
        }
    }
    //lists all lunched ocr tables jobs(using RestSharp)
    public static void ListAllJobs_RestSharp(string api_key)
    {
      var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest("ocr/ocr_tables_async", Method.Get);

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {api_key}");

        var response = client.Execute(request);
        Console.WriteLine(response.Content);
    }
    //lists all lucnhed ocr tables jobs(using HttpClient)
    public static void ListAllJobs_HttpClient(string api_key)
    {
      var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.edenai.run/v2/ocr/ocr_tables_async"),
            Headers =
    {
        { "authorization", $"Bearer {api_key}" },
        { "accept", "application/json" },
    },
        };
        using (var response = client.Send(request))
        {
            var body = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
        }
    }
}