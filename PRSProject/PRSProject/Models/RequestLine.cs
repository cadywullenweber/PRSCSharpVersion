using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRSProject.Models
{
    public class RequestLine
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)] //Quantity cannot be negative, min value 0
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual Product? Product { get; set; }

        [JsonIgnore]
        public virtual Request? Request { get; set; }


    }
}
