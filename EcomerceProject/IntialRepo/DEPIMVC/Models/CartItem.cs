using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DEPIMVC.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        [JsonProperty("added_At")] // Ensure this matches API's expected name
        public DateTime Added_At { get; set; } = DateTime.Now;

        [JsonProperty("productVartionID")] // Match API's spelling
        public int? ProductVartionID { get; set; }

        public int ShoppingCartId { get; set; }

        [JsonProperty("productVartion")] // Match API's spelling
        public ProductVationVm? ProductVartion { get; set; }
    }

}
