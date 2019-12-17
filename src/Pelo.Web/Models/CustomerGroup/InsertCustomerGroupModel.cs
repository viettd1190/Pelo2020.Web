using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pelo.Web.Models.CustomerGroup
{
    public class InsertCustomerGroupModel
    {
        [JsonProperty("name")]
        [Required(ErrorMessage = "Tên nhóm khách hàng không được để trống")]
        public string Name { get; set; }
    }
}
