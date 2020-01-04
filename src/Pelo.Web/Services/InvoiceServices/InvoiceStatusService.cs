using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.InvoiceStatus;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.InvoiceServices
{
    public interface IInvoiceStatusService
    {
        Task<TResponse<IEnumerable<InvoiceStatusSimpleModel>>> GetAll();
    }

    public class InvoiceStatusService : BaseService,
                                        IInvoiceStatusService
    {
        public InvoiceStatusService(IHttpService httpService) : base(httpService)
        {
        }

        #region IInvoiceStatusService Members

        public async Task<TResponse<IEnumerable<InvoiceStatusSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.INVOICE_STATUS_GET_ALL;
                var response = await HttpService.Send<IEnumerable<InvoiceStatusSimpleModel>>(url,
                                                                                             null,
                                                                                             HttpMethod.Get,
                                                                                             true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<InvoiceStatusSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<InvoiceStatusSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
