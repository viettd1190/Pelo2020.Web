using System.ComponentModel.DataAnnotations;

namespace Pelo.Web.Models.Customer
{
    public class FindCustomerByPhoneViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string NextAction { get; set; }
    }
}
