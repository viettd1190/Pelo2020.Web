using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CustomerSource;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CustomerServices
{
    public interface ICustomerSourceService
    {
        Task<TResponse<IEnumerable<CustomerSourceSimpleModel>>> GetAll();
    }

    public class CustomerSourceService : BaseService,
                                         ICustomerSourceService
    {
        public CustomerSourceService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICustomerSourceService Members

        public async Task<TResponse<IEnumerable<CustomerSourceSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.CUSTOMER_SOURCE_GET_ALL;
                var response = await HttpService.Send<IEnumerable<CustomerSourceSimpleModel>>(url,
                                                                                              null,
                                                                                              HttpMethod.Get,
                                                                                              true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<CustomerSourceSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<CustomerSourceSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
