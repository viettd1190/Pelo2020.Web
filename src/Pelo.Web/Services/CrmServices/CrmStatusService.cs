using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CrmStatus;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CrmServices
{
    public interface ICrmStatusService
    {
        Task<TResponse<IEnumerable<CrmStatusSimpleModel>>> GetAll();

        Task<TResponse<DatatableResponse<GetCrmStatusPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertCrmStatus request);

        Task<TResponse<GetCrmStatusResponse>> GetCrmById(int id);

        Task<TResponse<bool>> Update(UpdateCrmStatus request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class CrmStatusService : BaseService,
                                    ICrmStatusService
    {
        public CrmStatusService(IHttpService httpService) : base(httpService)
        {
        }

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CRM_STATUS_DELETE,
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

        #region ICrmStatusService Members

        public async Task<TResponse<IEnumerable<CrmStatusSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.CRM_STATUS_GET_ALL;
                var response = await HttpService.Send<IEnumerable<CrmStatusSimpleModel>>(url,
                                                                                         null,
                                                                                         HttpMethod.Get,
                                                                                         true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<CrmStatusSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<CrmStatusSimpleModel>>(exception);
            }
        }

        #endregion

        public async Task<TResponse<DatatableResponse<GetCrmStatusPagingResponse>>> GetByPaging(DatatableRequest request)
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

                var url = string.Format(ApiUrl.CRM_STATUS_PAGING,
                                        name,
                                        null, null,
                                        start,
                                        request?.Length ?? 10);

                var response = await HttpService.Send<PageResult<GetCrmStatusPagingResponse>>(url,
                                                                                        null,
                                                                                        HttpMethod.Get,
                                                                                        true);

                if (response.IsSuccess)
                    return await Ok(new DatatableResponse<GetCrmStatusPagingResponse>
                    {
                        Draw = request?.Draw ?? 1,
                        RecordsFiltered = response.Data.TotalCount,
                        RecordsTotal = response.Data.TotalCount,
                        Data = response.Data.Data.ToList()
                    });

                return await Fail<DatatableResponse<GetCrmStatusPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetCrmStatusPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<GetCrmStatusResponse>> GetCrmById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.GET_CRM_STATUS_ID,
                                        id);
                var response = await HttpService.Send<GetCrmStatusResponse>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCrmStatusResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCrmStatusResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertCrmStatus request)
        {
            try
            {
                var url = ApiUrl.CRM_STATUS_INSERT;
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

        public async Task<TResponse<bool>> Update(UpdateCrmStatus request)
        {
            try
            {
                var url = ApiUrl.CRM_STATUS_UPDATE;
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
    }
}
