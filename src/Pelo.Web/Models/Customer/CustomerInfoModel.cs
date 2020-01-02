using Pelo.Common.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.Web.Models.Customer
{
    public class CustomerInfoModel
    {
        public CustomerInfoModel(CustomerByPhoneResponse model) {
            Id = model.Id;
            Code = model.Code;
            Name = model.Name;
            Phone = model.Phone;
            Phone2 = model.Phone2;
            Phone3 = model.Phone3;
            Address = model.Address;
            CustomerGroup = model.CustomerGroup;
            CustomerVip = model.CustomerVip;
            Ward = model.Ward;
            District = model.District;
            Province = model.Province;
            Description = model.Description;
        }
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string CustomerGroup { get; set; }

        public string CustomerVip { get; set; }

        public string Phone { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public string Address{ get; set; }

        public string Ward { get; set; }

        public string District { get; set; }

        public string Province { get; set; }

        public string Description { get; set; }
    }
}
