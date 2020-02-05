using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.Web.Models.CrmStatus
{
    public class CrmStatusModel
    {
        public int Id { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsSendSms { get; set; }

        public string SmsContent { get; set; }
    }
}
