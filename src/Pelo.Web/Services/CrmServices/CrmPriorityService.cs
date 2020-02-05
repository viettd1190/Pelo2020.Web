using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CrmPriority;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.CrmServices;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CrmServices
{
    public interface ICrmPriorityService
    {
        Task<TResponse<IEnumerable<CrmPrioritySimpleModel>>> GetAll();
        Task<TResponse<DatatableResponse<GetCrmPriorityPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertCrmPriority request);

        Task<TResponse<GetCrmPriorityResponse>> GetCrmById(int id);

        Task<TResponse<bool>> Update(UpdateCrmPriority request);

        Task<TResponse<bool>> Delete(int id);
    }
}

    public class CrmPriorityService : BaseService,
                                      ICrmPriorityService
{
        public CrmPriorityService(IHttpService httpService) : base(httpService)
        {
        }

    public async Task<TResponse<bool>> Delete(int id)
    {
        try
        {
            var url = string.Format(ApiUrl.CRM_PRIORITY_DELETE,
                                    id);
            var response = await HttpService.Send<bool>(url,
                                                        null,
                                                        HttpMethod.Delete,
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

    #region ICrmPriorityService Members

    public async Task<TResponse<IEnumerable<CrmPrioritySimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.CRM_PRIORITY_GET_ALL;
                var response = await HttpService.Send<IEnumerable<CrmPrioritySimpleModel>>(url,
                                                                                           null,
                                                                                           HttpMethod.Get,
                                                                                           true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<CrmPrioritySimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<CrmPrioritySimpleModel>>(exception);
            }
        }

    public async Task<TResponse<DatatableResponse<GetCrmPriorityPagingResponse>>> GetByPaging(DatatableRequest request)
    {
        try
        {
            var name = string.Empty;
            if (request?.Columns != null)
                if (request.Columns.Any())
                {
                    name = request.Columns[0]
                                  .Search?.Value ?? string.Empty;
                }

            var start = 1;

            if (request != null) start = request.Start / request.Length + 1;

            var url = string.Format(ApiUrl.CRM_PRIORITY_PAGING,
                                    name,
                                    null, null,
                                    start,
                                    request?.Length ?? 10);

            var response = await HttpService.Send<PageResult<GetCrmPriorityPagingResponse>>(url,
                                                                                    null,
                                                                                    HttpMethod.Get,
                                                                                    true);

            if (response.IsSuccess)
                return await Ok(new DatatableResponse<GetCrmPriorityPagingResponse>
                {
                    Draw = request?.Draw ?? 1,
                    RecordsFiltered = response.Data.TotalCount,
                    RecordsTotal = response.Data.TotalCount,
                    Data = response.Data.Data.ToList()
                });

            return await Fail<DatatableResponse<GetCrmPriorityPagingResponse>>(response.Message);
        }
        catch (Exception exception)
        {
            return await Fail<DatatableResponse<GetCrmPriorityPagingResponse>>(exception);
        }
    }

    public async Task<TResponse<GetCrmPriorityResponse>> GetCrmById(int id)
    {
        try
        {
            var url = string.Format(ApiUrl.GET_CRM_PRIORITY_ID,
                                    id);
            var response = await HttpService.Send<GetCrmPriorityResponse>(url,
                                                        null,
                                                        HttpMethod.Get,
                                                        true);
            if (response.IsSuccess)
            {
                return await Ok(response.Data);
            }

            return await Fail<GetCrmPriorityResponse>(response.Message);
        }
        catch (Exception exception)
        {
            return await Fail<GetCrmPriorityResponse>(exception);
        }
    }

    public async Task<TResponse<bool>> Insert(InsertCrmPriority request)
    {
        try
            {
            var url = ApiUrl.CRM_PRIORITY_INSERT;
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

    public async Task<TResponse<bool>> Update(UpdateCrmPriority request)
    {
        try
        {
            var url = ApiUrl.CRM_PRIORITY_UPDATE;
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
