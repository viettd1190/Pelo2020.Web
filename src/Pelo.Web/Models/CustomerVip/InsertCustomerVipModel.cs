using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pelo.Web.Models.CustomerVip
{
    public class InsertCustomerVipModel
    {
        [JsonProperty("name")]
        [Required(ErrorMessage = "Tên mức độ thân thiết không được để trống")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("profit")]
        public int Profit { get; set; }
    }
}
