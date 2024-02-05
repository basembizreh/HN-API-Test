using Microsoft.AspNetCore.Mvc;

namespace HN_API_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HNController : ControllerBase
    {
        private readonly APIService apiService;

        public HNController(APIService ApiService)
        {
            this.apiService = ApiService;
        }

        private async Task<int[]> GetStoriesID(int numberOfStories)
        {
            var response = await apiService.CreateSingleRequest($"beststories.json?orderBy=\"$priority\"&limitToFirst={numberOfStories}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<int[]>();
                if (result != null)
                {
                    return result;
                }
            }
            return await Task.FromResult<int[]>(new int[] { });
        }

        [HttpGet]
        [Route(nameof(GetStories))]
        public async Task<List<object>> GetStories(int numberOfStories)
        {
            var results = new List<object>();
            // retrieve a list of n stories IDs sorted by story score
            int[] IDs = await GetStoriesID(numberOfStories);

            if (IDs != null && IDs.Length > 0)
            {
                // generate array of story details endpoints for the requested stories
                var arr = IDs.Select(p => $"item/{p}.json").ToArray();
                var responses = await apiService.CreateRequests(arr);

                foreach (var response in responses)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var story = await response.Content.ReadFromJsonAsync<Story>();
                        results.Add(new
                        {
                            story.Title,
                            Uri = story.Url,
                            PostedBy = story.By,
                            Time = FromEpoch(story.Time),
                            story.Score,
                            CommentCount = story.Descendants
                        });
                    }
                }
            }
            return results;
        }

        private DateTime FromEpoch(int value)
        {
            return DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
        }

        
    }
}
