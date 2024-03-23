using System;
using System.Text.Json;

namespace NCA.Production.Tests
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("enter");
            Console.ReadLine();

            //Con
            await Tests("http://localhost:5001");
            //Min
            await Tests("http://localhost:5002");

            Console.WriteLine("Terminado");
            Console.ReadLine();
        }

        static async Task Tests(string url)
        {
            Console.WriteLine($"Tests {url}");

            try
            {
                await Get(url);
                var id = await Post(url);
                await Put(url, id);
                await Delete(url, id);

                Console.WriteLine("Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error");
            }

            Console.WriteLine($"Tests {url}");
        }

        static async Task Get(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url + "/api/v1/ProductCategories");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        static async Task<string> Post(string url)
        {
            int r = new Random().Next(0, 1000);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url + "/api/v1/ProductCategories");
            var data = new { Name = "test" + r };
            var content = new StringContent(JsonSerializer.Serialize(data), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        static async Task Put(string url, string id)
        {
            int r = new Random().Next(0, 1000);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, url + "/api/v1/ProductCategories");
            var data = new { ProductCategoryId = id, Name = "test" + r };
            var content = new StringContent(JsonSerializer.Serialize(data), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }

        static async Task Delete(string url, string id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, url + "/api/v1/ProductCategories/" + id);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }
    }
}
