using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestSharp;
public class SpeechToText
{
    //Lunch job by passing file url (using RestSharp)
    public static void LunchJob_RestSharp(string api_key)
    {
        var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest("audio/speech_to_text_async", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("authorization", $"Bearer {api_key}");
        request.AddHeader("content-type", "application/json");
        request.AddParameter("application/json", "{\"show_original_response\":false,\"speakers\":2,\"providers\":\"google\",\"file_url\":\"file_url_here\",\"profanity_filter\":false,\"convert_to_wav\":false}", ParameterType.RequestBody);
        string response = client.Execute(request).Content;
    }
    //Lunch job by passing a local file path (using RestSharp)
    public static void LunchJob_RestSharp(string api_key, string file_path)
    {
        var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest("audio/speech_to_text_async", Method.Post);


        request.AddHeader("Authorization", $"Bearer {api_key}");

        request.AddParameter("providers", "microsoft");
        request.AddParameter("language", "en");

        byte[] fileBytes = File.ReadAllBytes(file_path);
        request.AddFile("file", fileBytes, Path.GetFileName(file_path));

        var response = client.Execute(request);

        string responseContent = response.Content;
    }
    //Lunch job by passing file url(using HttpClient)
    public static void LunchJob_HttpClient(string api_key)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.edenai.run/v2/audio/speech_to_text_async"),
            Headers =
    {
        { "accept", "application/json" },
        { "authorization", $"Bearer {api_key}" },
    },
            Content = new StringContent("{\"show_original_response\":false,\"providers\":\"google\",\"speakers\":2,\"profanity_filter\":false,\"convert_to_wav\":false,\"file_url\":\"YOUR_FILE_URL_HERE\"}")
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
    //Lunch a job by passing local file path (using HttpClient)
    public static void LunchJob_HttpClient(string api_key, string file_path)
    {
        var client = new HttpClient();
        var content = new MultipartFormDataContent();

        content.Add(new StreamContent(File.OpenRead(file_path)), "file", Path.GetFileName(file_path));
        content.Add(new StringContent("microsoft"), "providers");
        content.Add(new StringContent("en"), "language");

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.edenai.run/v2/audio/speech_to_text_async"));
        request.Content = content;
        request.Headers.Add("Authorization", $"Bearer {api_key}");
        using (var response = client.Send(request))
        {
            var body = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
        }
    }
    //Get speech to text job results (using RestSharp)
    public static void GetJob_RestSharp(string api_key, string id)
    {
        var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest($"audio/speech_to_text_async/{id}", Method.Get);

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {api_key}");

        request.AddParameter("response_as_dict", true);
        request.AddParameter("show_original_response", true);



        var response = client.Execute(request);
        Console.WriteLine(response.Content);
    }
    //Get speech to text job results(using HttpClient)
    public static void GetJob_HttpClient(string api_key, string id)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.edenai.run/v2/audio/speech_to_text_async/{id}"),
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
    //Lists all speech to text lunched jobs (using RestSharp)
    public static void ListAllJobs_RestSharp(string api_key)
    {
        var client = new RestClient("https://api.edenai.run/v2");
        var request = new RestRequest("audio/speech_to_text_async", Method.Get);

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {api_key}");

        var response = client.Execute(request);
        Console.WriteLine(response.Content);
    }
    //Lists all speech to text lunched jobs (using HttpClient)
    public static void ListAllJobs_HttpClient(string api_key)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.edenai.run/v2/audio/speech_to_text_async"),
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