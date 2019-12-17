using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pelo.Web.Models.CustomerGroup
{
    public class CustomerGroupModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Tên nhóm khách hàng không được để trống")]
        public string Name { get; set; }
    }
}
