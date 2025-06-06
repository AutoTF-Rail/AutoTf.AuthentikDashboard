using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace AutoTf.AuthentikDashboard.Models;

public static class HttpHelper
{
    /// <summary>
    /// Sends a GET request to the given endpoint and returns it's content as a string.
    /// It is not required to add the authentik username header in here, because that's automatically done due to all requests being routed through authentik.
    /// </summary>
    public static async Task<string> SendGetString(string endpoint, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            
            if (Statics.Token != string.Empty)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);
            
            HttpResponseMessage response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            if(reThrow)
                throw;

            return string.Empty;
        }
    }
    
    /// <summary>
    /// Sends a GET request to the given endpoint and returns it's content as the given type.
    /// </summary>
    public static async Task<T?> SendGet<T>(string endpoint, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            
            if (Statics.Token != string.Empty)  
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);
            
            HttpResponseMessage response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }
        catch
        {
            if(reThrow)
                throw;

            return default;
        }
    }
    
    /// <summary>
    /// Sends a GET request to the given endpoint and returns it's content as a string.
    /// </summary>
    public static async Task<T?> SendPost<T>(string endpoint, HttpContent content, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;
            
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            
            if (Statics.Token != string.Empty)  
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);
            
            HttpResponseMessage response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }
        catch
        {
            if(reThrow)
                throw;

            return default;
        }
    }

    public static async Task<bool> SendDelete(string endpoint, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;

            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

            if (Statics.Token != string.Empty)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);
            
            HttpResponseMessage response = await client.DeleteAsync(endpoint);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            if (reThrow)
                throw;
            else
            {
                Console.WriteLine(ex.ToString());
            }
            
            return false;
        }
    }

    public static async Task<string> SendPut(string endpoint, HttpContent content, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;

            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

            if (Statics.Token != string.Empty)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);

            HttpResponseMessage response = await client.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            if (reThrow)
                throw;

            return string.Empty;
        }
    }

    public static async Task<T?> SendPut<T>(string endpoint, HttpContent content, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;

            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

            if (Statics.Token != string.Empty)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);
            
            HttpResponseMessage response = await client.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            
            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }
        catch
        {
            if (reThrow)
                throw;

            return default;
        }
    }

    public static async Task<string> SendPatch(string endpoint, HttpContent content, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;

            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

            if (Statics.Token != string.Empty)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);
            
            HttpResponseMessage response = await client.PatchAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            if (reThrow)
                throw;

            return string.Empty;
        }
    }

    public static async Task<T?> SendPatch<T>(string endpoint, HttpContent content, bool reThrow = false, int timeoutSeconds = 5)
    {
        try
        {
            endpoint = Statics.AuthUrl + endpoint;

            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

            if (Statics.Token != string.Empty)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Token);

            HttpResponseMessage response = await client.PatchAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }
        catch
        {
            if (reThrow)
                throw;

            return default;
        }
    }
}