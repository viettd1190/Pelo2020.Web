using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pelo.Web.Models.User
{
    public class InsertUserModel
    {
        [JsonProperty("username")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

        [JsonProperty("display_name")] public string DisplayName { get; set; }

        [JsonProperty("full_name")] public string FullName { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("branch_id")] public int BranchId { get; set; }

        [JsonProperty("role_id")] public int RoleId { get; set; }

        [JsonProperty("department_id")] public int DepartmentId { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}
