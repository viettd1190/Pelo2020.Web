using Pelo.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.Web.Models.Crm
{
    public class UpdateCrmModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string Need { get; set; }

        public string Description { get; set; }

        public string ContactDate { get; set; }

        public string ContactTime { get; set; }

        public int CrmStatusId { get; set; }

        public int ProductGroupId { get; set; }

        public int CrmPriorityId { get; set; }

        public int CustomerSourceId { get; set; }

        public int CrmTypeId { get; set; }

        public int Visit { get; set; }

        public int[] UserIds { get; set; }

        public string Address { get; set; }

        public string Code { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string CustomerGroup { get; set; }

        public string CustomerVip { get; set; }

        public string CustomerDescription { get; set; }

        public string UserCreated { get; set; }

        public string DateCreated { get; set; }

    }
}
