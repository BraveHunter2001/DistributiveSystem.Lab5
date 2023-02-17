using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text;

namespace DistributiveSystem.Lab5.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; private set; } = "";
     

        static HttpClient httpClient = new HttpClient();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public  void OnPost(int amount, string rate)
        {

                float resultRate;
                if (float.TryParse(rate, out resultRate))
                    Message = GetResult(amount, resultRate);
                else
                    Message = "Error parse rate field";

        }

        public string GetResult(int amount, float rate)
        {
            string url = $"https://localhost:7101/service?amount={amount}&rate={rate.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = httpClient.Send(request);
            var stream = response.Content.ReadAsStream();
            stream.Position = 0;
            string ans;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                ans = reader.ReadToEnd();
            }
            return ans;
        }
    }
}