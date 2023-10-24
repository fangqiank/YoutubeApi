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
        [HttpGet]
        public async Task<IActionResult> GetChannelVidoes(string? pageToken = null, int maxResults = 5)
        {
            var service = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = "TestYouTube",
                ApiKey = "AIzaSyBRs93Zal11jSavfzM2JllMHlLYXWL6Afo"
            });

            var searchRequest = service.Search.List("snippet");
            searchRequest.ChannelId = "UCC_dVe-RI-vgCZfls06mDZQ";
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
