using Pelo.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
  
          public string UserIds { get; set; }

        public string Code { get; set; }
        public CustomerInfoModel CustomerInfoModel { get; set; }

    }
}
