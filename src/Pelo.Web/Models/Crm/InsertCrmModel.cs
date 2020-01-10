﻿using Pelo.Web.Models.Customer;

namespace Pelo.Web.Models.Crm
{
    public class InsertCrmModel
    {
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

        public string Code { get; set; }

        public CustomerInfoModel CustomerInfoModel { get; set; }

        public string Phone { get; set; }
    }
}
