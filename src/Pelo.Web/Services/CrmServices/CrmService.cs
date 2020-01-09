using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Crm;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CrmServices
{
    public interface ICrmService
    {
        Task<TResponse<DatatableResponse<GetCrmPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertCrmRequest request);

        Task<TResponse<GetCrmModelReponse>> GetCrmById(int id);

        Task<TResponse<bool>> Update(UpdateCrmRequest request);
    }

    public class CrmService : BaseService,
                              ICrmService
    {
        public CrmService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICrmService Members

        public async Task<TResponse<DatatableResponse<GetCrmPagingResponse>>> GetByPaging(DatatableRequest request)
        {
            try
            {
                var code = string.Empty;
                var customerCode = string.Empty;
                var customerName = string.Empty;
                var customerPhone = string.Empty;
                var customerAddress = string.Empty;
                int provinceId = 0;
                int districtId = 0;
                int wardId = 0;
                int customerGroupId = 0;
                int customerVipId = 0;
                int customerSourceId = 0;
                int productGroupId = 0;
                int crmStatusId = 0;
                int crmTypeId = 0;
                int crmPriorityId = 0;
                int visit = -1;
                DateTime? fromDate = null;
                DateTime? toDate = null;
                int userCreatedId = 0;
                DateTime? dateCreated = null;
                int userCareId = 0;
                string need = string.Empty;

                if (request?.Columns != null)
                    if (request.Columns.Any())
                    {
                        code = request.Columns[0]
                                      .Search?.Value ?? string.Empty;
                        customerCode = request.Columns[1]
                                              .Search?.Value ?? string.Empty;
                        customerName = request.Columns[2]
                                              .Search?.Value ?? string.Empty;
                        customerPhone = request.Columns[3]
                                               .Search?.Value ?? string.Empty;
                        customerAddress = request.Columns[4]
                                                 .Search?.Value ?? string.Empty;

                        var province = request.Columns[5]
                                              .Search?.Value ?? string.Empty;
                        var district = request.Columns[6]
                                              .Search?.Value ?? string.Empty;
                        var ward = request.Columns[7]
                                          .Search?.Value ?? string.Empty;
                        var customerGroup = request.Columns[8]
                                                   .Search?.Value ?? string.Empty;
                        var customerVip = request.Columns[9]
                                                 .Search?.Value ?? string.Empty;
                        var customerSource = request.Columns[10]
                                                    .Search?.Value ?? string.Empty;
                        var productGroup = request.Columns[11]
                                                  .Search?.Value ?? string.Empty;
                        var crmStatus = request.Columns[12]
                                               .Search?.Value ?? string.Empty;
                        var crmType = request.Columns[13]
                                             .Search?.Value ?? string.Empty;
                        var crmPriority = request.Columns[14]
                                                 .Search?.Value ?? string.Empty;
                        var sVisit = request.Columns[15]
                                            .Search?.Value ?? string.Empty;
                        var fromContactDate = request.Columns[16]
                                                     .Search?.Value ?? string.Empty;
                        var toContactDate = request.Columns[17]
                                                   .Search?.Value ?? string.Empty;
                        var userCreated = request.Columns[18]
                                                 .Search?.Value ?? string.Empty;
                        var sDateCreated = request.Columns[19]
                                                  .Search.Value ?? string.Empty;
                        var userCare = request.Columns[20]
                                              .Search.Value ?? string.Empty;
                        need = request.Columns[21]
                                      .Search?.Value ?? string.Empty;

                        if (!string.IsNullOrEmpty(province))
                        {
                            int.TryParse(province,
                                         out provinceId);
                        }

                        if (!string.IsNullOrEmpty(district))
                        {
                            int.TryParse(district,
                                         out districtId);
                        }

                        if (!string.IsNullOrEmpty(ward))
                        {
                            int.TryParse(ward,
                                         out wardId);
                        }

                        if (!string.IsNullOrEmpty(customerGroup))
                        {
                            int.TryParse(customerGroup,
                                         out customerGroupId);
                        }

                        if (!string.IsNullOrEmpty(customerVip))
                        {
                            int.TryParse(customerVip,
                                         out customerVipId);
                        }

                        if (!string.IsNullOrEmpty(customerSource))
                        {
                            int.TryParse(customerSource,
                                         out customerSourceId);
                        }

                        if (!string.IsNullOrEmpty(productGroup))
                        {
                            int.TryParse(productGroup,
                                         out productGroupId);
                        }

                        if (!string.IsNullOrEmpty(crmStatus))
                        {
                            int.TryParse(crmStatus,
                                         out crmStatusId);
                        }

                        if (!string.IsNullOrEmpty(crmType))
                        {
                            int.TryParse(crmType,
                                         out crmTypeId);
                        }

                        if (!string.IsNullOrEmpty(crmPriority))
                        {
                            int.TryParse(crmPriority,
                                         out crmPriorityId);
                        }

                        if (!string.IsNullOrEmpty(sVisit))
                        {
                            int.TryParse(sVisit,
                                         out visit);
                        }

                        DateTime tmpDate;

                        if (!string.IsNullOrEmpty(fromContactDate))
                        {
                            if (DateTime.TryParse(fromContactDate,
                                                 out tmpDate))
                            {
                                fromDate = tmpDate;
                            }
                        }

                        if (!string.IsNullOrEmpty(toContactDate))
                        {
                            if (DateTime.TryParse(toContactDate,
                                                 out tmpDate))
                            {
                                toDate = tmpDate;
                            }
                        }

                        if (!string.IsNullOrEmpty(userCreated))
                        {
                            int.TryParse(userCreated,
                                         out userCreatedId);
                        }

                        if (!string.IsNullOrEmpty(sDateCreated))
                        {
                            if (DateTime.TryParse(sDateCreated,
                                                 out tmpDate))
                            {
                                dateCreated = tmpDate;
                            }
                        }

                        if (!string.IsNullOrEmpty(userCare))
                        {
                            int.TryParse(userCare,
                                         out userCareId);
                        }
                    }

                var start = 1;

                if (request != null) start = request.Start / request.Length + 1;

                var url = string.Format(ApiUrl.CRM_GET_BY_PAGING,
                                        code,
                                        customerCode,
                                        customerName,
                                        customerPhone,
                                        customerAddress,
                                        provinceId,
                                        districtId,
                                        wardId,
                                        customerGroupId,
                                        customerVipId,
                                        customerSourceId,
                                        productGroupId,
                                        crmStatusId,
                                        crmTypeId,
                                        crmPriorityId,
                                        visit,
                                        fromDate,
                                        toDate,
                                        userCreatedId,
                                        dateCreated,
                                        userCareId,
                                        need,
                                        start,
                                        request?.Length ?? 10);

                var response = await HttpService.Send<PageResult<GetCrmPagingResponse>>(url,
                                                                                        null,
                                                                                        HttpMethod.Get,
                                                                                        true);

                if (response.IsSuccess)
                    return await Ok(new DatatableResponse<GetCrmPagingResponse>
                    {
                        Draw = request?.Draw ?? 1,
                        RecordsFiltered = response.Data.TotalCount,
                        RecordsTotal = response.Data.TotalCount,
                        Data = response.Data.Data.ToList()
                    });

                return await Fail<DatatableResponse<GetCrmPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetCrmPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertCrmRequest request)
        {
            try
            {
                var url = ApiUrl.CRM_INSERT;
                var response = await HttpService.Send<bool>(url,
                                                            request,
                                                            HttpMethod.Post,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(true);
                }

                return await Fail<bool>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }
        public async Task<TResponse<GetCrmModelReponse>> GetCrmById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.GET_CRM_ID,
                                        id);
                var response = await HttpService.Send<GetCrmModelReponse>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCrmModelReponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCrmModelReponse>(exception);
            }
        }
        public async Task<TResponse<bool>> Update(UpdateCrmRequest request)
        {
            try
            {
                var url = ApiUrl.CRM_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            request,
                                                            HttpMethod.Put,
                                                              true);
                if (response.IsSuccess)
                {
                    return await Ok(true);
                }

                return await Fail<bool>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }
        #endregion
    }
}
