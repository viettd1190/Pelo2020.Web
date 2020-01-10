using System;
using Pelo.Web.Models.Customer;

namespace Pelo.Web.Models.Invoice
{
    public class InsertInvoiceModel
    {
        public int CustomerId { get; set; }

        public CustomerInfoModel CustomerInfoModel { get; set; }

        public string Products { get; set; }

        public int Discount { get; set; }

        public int PayMethodId { get; set; }

        public int Deposit { get; set; }

        public int UserSellId { get; set; }

        public int BranchId { get; set; }

        public DateTime DeliveryDate { get; set; }


        public string Description { get; set; }
    }
}
