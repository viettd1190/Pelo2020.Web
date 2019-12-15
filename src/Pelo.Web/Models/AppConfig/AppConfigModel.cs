using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pelo.Web.Models.AppConfig
{
    public class AppConfigModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Tên cấu hình không được để trống")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
