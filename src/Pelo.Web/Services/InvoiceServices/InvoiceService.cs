using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Invoice;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.InvoiceServices
{
    public interface IInvoiceService
    {
        Task<TResponse<DatatableResponse<GetInvoicePagingResponse>>> GetByPaging(DatatableRequest request);
    }

    public class InvoiceService : BaseService,
                                  IInvoiceService
    {
        public InvoiceService(IHttpService httpService) : base(httpService)
        {
        }

        #region IInvoiceService Members

        public async Task<TResponse<DatatableResponse<GetInvoicePagingResponse>>> GetByPaging(DatatableRequest request)
        {
            try
            {
                var code = string.Empty;
                var customerCode = string.Empty;
                var customerName = string.Empty;
                var customerPhone = string.Empty;
                int branchId = 0;
                int invoiceStatusId = 0;
                int userCreatedId = 0;
                int userSellId = 0;
                int userDeliveryId = 0;
                DateTime? fromDate = null;
                DateTime? toDate = null;

                if(request?.Columns != null)
                    if(request.Columns.Any())
                    {
                        customerCode = request.Columns[0]
                                              .Search?.Value ?? string.Empty;
                        customerPhone = request.Columns[1]
                                               .Search?.Value ?? string.Empty;
                        customerName = request.Columns[2]
                                              .Search?.Value ?? string.Empty;
                        code = request.Columns[3]
                                      .Search?.Value ?? string.Empty;
                        var branch = request.Columns[4]
                                            .Search?.Value ?? string.Empty;
                        var invoiceStatus = request.Columns[5]
                                                   .Search?.Value ?? string.Empty;
                        var userCreated = request.Columns[6]
                                                 .Search?.Value ?? string.Empty;
                        var userSell = request.Columns[7]
                                              .Search?.Value ?? string.Empty;
                        var userDelivery = request.Columns[8]
                                                  .Search?.Value ?? string.Empty;
                        var fromDateCreated = request.Columns[9]
                                                     .Search?.Value ?? string.Empty;
                        var toDateCreated = request.Columns[10]
                                                   .Search?.Value ?? string.Empty;

                        if(!string.IsNullOrEmpty(branch))
                        {
                            int.TryParse(branch,
                                         out branchId);
                        }

                        if(!string.IsNullOrEmpty(invoiceStatus))
                        {
                            int.TryParse(invoiceStatus,
                                         out userDeliveryId);
                        }

                        if(!string.IsNullOrEmpty(userDelivery))
                        {
                            int.TryParse(userDelivery,
                                         out userDeliveryId);
                        }

                        if(!string.IsNullOrEmpty(userCreated))
                        {
                            int.TryParse(userCreated,
                                         out userCreatedId);
                        }

                        if(!string.IsNullOrEmpty(userSell))
                        {
                            int.TryParse(userSell,
                                         out userSellId);
                        }

                        DateTime tmpDate;

                        if(!string.IsNullOrEmpty(fromDateCreated))
                        {
                            if(DateTime.TryParse(fromDateCreated,
                                                 out tmpDate))
                            {
                                fromDate = tmpDate;
                            }
                        }

                        if(!string.IsNullOrEmpty(toDateCreated))
                        {
                            if(DateTime.TryParse(toDateCreated,
                                                 out tmpDate))
                            {
                                toDate = tmpDate;
                            }
                        }

                        if(!string.IsNullOrEmpty(userCreated))
                        {
                            int.TryParse(userCreated,
                                         out userCreatedId);
                        }
                    }

                var start = 1;

                if(request != null) start = request.Start / request.Length + 1;

                var url = string.Format(ApiUrl.INVOICE_GET_BY_PAGING,
                                        customerCode,
                                        customerPhone,
                                        customerName,
                                        code,
                                        branchId,
                                        invoiceStatusId,
                                        userCreatedId,
                                        userSellId,
                                        userDeliveryId,
                                        fromDate,
                                        toDate,
                                        start,
                                        request?.Length ?? 10);

                var response = await HttpService.Send<PageResult<GetInvoicePagingResponse>>(url,
                                                                                            null,
                                                                                            HttpMethod.Get,
                                                                                            true);

                if(response.IsSuccess)
                    return await Ok(new DatatableResponse<GetInvoicePagingResponse>
                                    {
                                            Draw = request?.Draw ?? 1,
                                            RecordsFiltered = response.Data.TotalCount,
                                            RecordsTotal = response.Data.TotalCount,
                                            Data = response.Data.Data.ToList()
                                    });

                return await Fail<DatatableResponse<GetInvoicePagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetInvoicePagingResponse>>(exception);
            }
        }

        #endregion
    }
}
