using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRSProject.Models
{
    public class Request
    {
        public int Id { get; set; }

        [StringLength(80)]
        public string Description { get; set; }

        [StringLength(80)]
        public string Justification { get; set; }

        [StringLength(80)]
        public string? RejectionReason { get; set; }

        [StringLength(20)]
        public string DeliveryMode { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Column(TypeName = "decimal(11, 2)")]
        public decimal Total { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public List<RequestLine>? RequestLines { get; set; }    


    }
}
