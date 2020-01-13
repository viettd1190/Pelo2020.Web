using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.PayMethod;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.InvoiceServices
{
    public interface IPayMethodService
    {
        Task<TResponse<IEnumerable<PayMethodSimpleModel>>> GetAll();
    }

    public class PayMethodService : BaseService,
                                    IPayMethodService
    {
        public PayMethodService(IHttpService httpService) : base(httpService)
        {
        }

        #region IPayMethodService Members

        public async Task<TResponse<IEnumerable<PayMethodSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.PAY_METHOD_GET_ALL;
                var response = await HttpService.Send<IEnumerable<PayMethodSimpleModel>>(url,
                                                                                         null,
                                                                                         HttpMethod.Get,
                                                                                         true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<PayMethodSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<PayMethodSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
