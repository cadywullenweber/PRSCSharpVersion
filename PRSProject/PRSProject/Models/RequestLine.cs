using System.Text.Json.Serialization;

namespace PRSProject.Models
{
    public class RequestLine
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public List<Product>? Products { get; set; }
    }
}
