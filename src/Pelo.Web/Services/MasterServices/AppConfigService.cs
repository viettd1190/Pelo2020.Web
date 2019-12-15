using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.AppConfig;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IAppConfigService
    {
        Task<TResponse<DatatableResponse<GetAppConfigPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertAppConfigRequest request);

        Task<TResponse<GetAppConfigByIdResponse>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateAppConfigRequest request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class AppConfigService : BaseService,
                                    IAppConfigService
    {
        public AppConfigService(IHttpService httpService) : base(httpService)
        {
        }

        #region IAppConfigService Members

        public async Task<TResponse<DatatableResponse<GetAppConfigPagingResponse>>> GetByPaging(DatatableRequest request)
        {
            try
            {
                var name = string.Empty;
                var description = string.Empty;
                if(request?.Columns != null)
                    if(request.Columns.Any())
                    {
                        name = request.Columns[0]
                                      .Search?.Value ?? string.Empty;
                        description = request.Columns[1]
                                             .Search?.Value ?? string.Empty;
                    }

                var start = 1;

                if(request != null) start = request.Start / request.Length + 1;

                var columnOrder = "name";
                var sortDir = "ASC";

                if(request?.Order != null
                   && request.Order.Any())
                    if(request.Columns != null)
                    {
                        columnOrder = request.Columns[request.Order[0]
                                                             .Column]
                                             .Data;
                        sortDir = request.Order[0]
                                         .Dir.ToUpper();
                    }

                var url = string.Format(ApiUrl.APP_CONFIG_GET_BY_PAGING,
                                        name,
                                        description,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetAppConfigPagingResponse>>(url,
                                                                                              null,
                                                                                              HttpMethod.Get,
                                                                                              true);

                if(response.IsSuccess)
                    return await Ok(new DatatableResponse<GetAppConfigPagingResponse>
                                    {
                                            Draw = request?.Draw ?? 1,
                                            RecordsFiltered = response.Data.TotalCount,
                                            RecordsTotal = response.Data.TotalCount,
                                            Data = response.Data.Data.ToList()
                                    });

                return await Fail<DatatableResponse<GetAppConfigPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetAppConfigPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertAppConfigRequest request)
        {
            try
            {
                var url = ApiUrl.APP_CONFIG_INSERT;
                var response = await HttpService.Send<bool>(url,
                                                            request,
                                                            HttpMethod.Post,
                                                            true);
                if(response.IsSuccess)
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

        public async Task<TResponse<GetAppConfigByIdResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.APP_CONFIG_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetAppConfigByIdResponse>(url,
                                                                                null,
                                                                                HttpMethod.Get,
                                                                                true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetAppConfigByIdResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetAppConfigByIdResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateAppConfigRequest request)
        {
            try
            {
                var url = ApiUrl.APP_CONFIG_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            request,
                                                            HttpMethod.Put,
                                                            true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<bool>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.APP_CONFIG_DELETE,
                                        id);
                var response = await HttpService.Send<bool>(url,
                                                            null,
                                                            HttpMethod.Delete,
                                                            true);
                if(response.IsSuccess)
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
