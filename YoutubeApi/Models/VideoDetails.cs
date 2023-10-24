namespace YoutubeApi.Models
{
    public class VideoDetails
    {
        public string Title { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public DateTimeOffset? PublishedAt { get; set; }
    }
}
