using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.Web.Models.Crm
{
    public class AddCommentCrm
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int CrmStatusId { get; set; }

        public IFormFile File { get; set; }
    }
}
