namespace YoutubeApi.Models
{
    public class YoutubeResponse
    {
        public List<VideoDetails> Videos { get; set; } = new();
        public string PrevPageToken { get; set; } = string.Empty;
        public string NextPageToken { get; set; } = string.Empty;
    }
}
