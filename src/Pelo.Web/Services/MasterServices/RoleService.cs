using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Role;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IRoleService
    {
        Task<TResponse<IEnumerable<RoleSimpleModel>>> GetAll();

        Task<TResponse<DatatableResponse<GetRolePagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertRole request);

        Task<TResponse<GetRoleReponse>> GetCrmById(int id);

        Task<TResponse<bool>> Update(UpdateRole request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IHttpService httpService) : base(httpService)
        {
        }

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.ROLE_DELETE,
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

        public async Task<TResponse<IEnumerable<RoleSimpleModel>>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<RoleSimpleModel>>(ApiUrl.ROLE_GET_ALL,
                    null,
                    HttpMethod.Get,
                    true);
                if (response.IsSuccess) return await Ok(response.Data);

                return await Fail<IEnumerable<RoleSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<RoleSimpleModel>>(exception);
            }
        }

        public async Task<TResponse<DatatableResponse<GetRolePagingResponse>>> GetByPaging(DatatableRequest request)
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

                var url = string.Format(ApiUrl.ROLE_PAGING,
                                        name,
                                        null, null,
                                        start,
                                        request?.Length ?? 10);

                var response = await HttpService.Send<PageResult<GetRolePagingResponse>>(url,
                                                                                        null,
                                                                                        HttpMethod.Get,
                                                                                        true);

                if (response.IsSuccess)
                    return await Ok(new DatatableResponse<GetRolePagingResponse>
                    {
                        Draw = request?.Draw ?? 1,
                        RecordsFiltered = response.Data.TotalCount,
                        RecordsTotal = response.Data.TotalCount,
                        Data = response.Data.Data.ToList()
                    });

                return await Fail<DatatableResponse<GetRolePagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetRolePagingResponse>>(exception);
            }
        }

        public async Task<TResponse<GetRoleReponse>> GetCrmById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.GET_ROLE_ID,
                                        id);
                var response = await HttpService.Send<GetRoleReponse>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetRoleReponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetRoleReponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertRole request)
        {
            try
            {
                var url = ApiUrl.ROLE_INSERT;
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

        public async Task<TResponse<bool>> Update(UpdateRole request)
        {
            try
            {
                var url = ApiUrl.ROLE_UPDATE;
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