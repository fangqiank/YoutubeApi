using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using YoutubeApi.Models;

namespace YoutubeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public YoutubeController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public async Task<IActionResult> GetChannelVidoes(string? pageToken = null, int maxResults = 5)
        {
            var service = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = "TestYouTube",
                ApiKey = _config.GetValue<string>("Youtube:ApiKey")
            }); 

            var searchRequest = service.Search.List("snippet");
            searchRequest.ChannelId = _config.GetValue<string>("Youtube:ChannelId");
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchRequest.MaxResults = maxResults;
            searchRequest.PageToken = pageToken;

            var response = await searchRequest.ExecuteAsync();

            var videoList = response.Items.Select(item => new VideoDetails
            {
                Title = item.Snippet.Title,
                Link = $"https://www.youtube.com/watch?y={item.Id.VideoId}",
                Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                PublishedAt = item.Snippet.PublishedAtDateTimeOffset
            })
                .OrderByDescending(v => v.PublishedAt)
                .ToList();

            var youTubeResponse = new YoutubeResponse
            {
                Videos = videoList,
                PrevPageToken = response.PrevPageToken,
                NextPageToken = response.NextPageToken
            };

            return Ok(youTubeResponse);
        } 
    }
}
