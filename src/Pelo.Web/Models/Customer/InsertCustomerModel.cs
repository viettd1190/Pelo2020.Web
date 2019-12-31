namespace Pelo.Web.Models.Customer
{
    public class InsertCustomerModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public int CustomerGroupId { get; set; }

        public string Description { get; set; }
    }
}
