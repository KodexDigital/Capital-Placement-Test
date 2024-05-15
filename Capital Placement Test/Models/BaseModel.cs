using Newtonsoft.Json;

namespace Capital_Placement_Test.Models
{
    public class BaseModel
    {
        [JsonProperty("clientKey")]
        public string? ClientKey { get; set; }
        public string? Id { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
            ClientKey = Id;
        }
    }
}