using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace DEPIMVC.Models
{
    public class ApplicationUser
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
