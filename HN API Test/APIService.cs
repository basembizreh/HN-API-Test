namespace HN_API_Test
{
    public class APIService
    {
        private string BaseURL = "https://hacker-news.firebaseio.com/v0";
        // Create an instance of HttpClient
        private HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> CreateSingleRequest(string apiUrl)
        {
            string url = $"{BaseURL}/{apiUrl}";
            // GET request to the Firebase API
            return await client.GetAsync(url);
        }

        public Task<HttpResponseMessage[]> CreateRequests(string[] endpoints)
        {
            // Create tasks to send requests asynchronously
            var responseTask = new Task<HttpResponseMessage>[endpoints.Length];
            for (int i = 0; i < endpoints.Length; i++)
            {
                responseTask[i] = client.GetAsync($"{BaseURL}/{endpoints[i]}");
            }
            // Waitaing for all the tasks to complete
            return Task.WhenAll(responseTask);
        }
    }
}
