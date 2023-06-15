namespace ToyStoreAPI.Models
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
