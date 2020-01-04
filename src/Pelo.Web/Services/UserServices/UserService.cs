using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.User;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.UserServices
{
    public interface IUserService
    {
        Task<TResponse<DatatableResponse<GetUserPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertUserRequest request);

        Task<TResponse<GetUserByIdResponse>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateUserRequest request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<IEnumerable<UserDisplaySimpleModel>>> GetAll();

        Task<TResponse<bool>> IsBelongDefaultCrmRole();

        Task<TResponse<bool>> IsBelongDefaultInvoiceRole();
    }

    public class UserService : BaseService,
                               IUserService
    {
        public UserService(IHttpService httpService) : base(httpService)
        {
        }

        #region IUserService Members

        public async Task<TResponse<DatatableResponse<GetUserPagingResponse>>> GetByPaging(DatatableRequest request)
        {
            try
            {
                var code = string.Empty;
                var fullName = string.Empty;
                var phoneNumber = string.Empty;
                var branchId = 0;
                var departmentId = 0;
                var roleId = 0;
                var status = -1;
                if(request?.Columns != null)
                    if(request.Columns.Any())
                    {
                        code = request.Columns[0]
                                      .Search?.Value ?? string.Empty;
                        fullName = request.Columns[1]
                                          .Search?.Value ?? string.Empty;
                        phoneNumber = request.Columns[2]
                                             .Search?.Value ?? string.Empty;
                        int.TryParse(request.Columns[3]
                                            .Search?.Value ?? string.Empty,
                                     out branchId);
                        int.TryParse(request.Columns[4]
                                            .Search?.Value ?? string.Empty,
                                     out departmentId);
                        int.TryParse(request.Columns[5]
                                            .Search?.Value ?? string.Empty,
                                     out roleId);
                        int.TryParse(request.Columns[6]
                                            .Search?.Value ?? string.Empty,
                                     out status);
                    }

                var start = 1;

                if(request != null) start = request.Start / request.Length + 1;

                var columnOrder = "code";
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

                var url = string.Format(ApiUrl.USER_GET_BY_PAGING,
                                        code,
                                        fullName,
                                        phoneNumber,
                                        branchId,
                                        departmentId,
                                        roleId,
                                        status,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetUserPagingResponse>>(url,
                                                                                         null,
                                                                                         HttpMethod.Get,
                                                                                         true);

                if(response.IsSuccess)
                    return await Ok(new DatatableResponse<GetUserPagingResponse>
                                    {
                                            Draw = request?.Draw ?? 1,
                                            RecordsFiltered = response.Data.TotalCount,
                                            RecordsTotal = response.Data.TotalCount,
                                            Data = response.Data.Data.ToList()
                                    });

                return await Fail<DatatableResponse<GetUserPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetUserPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertUserRequest request)
        {
            try
            {
                var url = ApiUrl.USER_INSERT;
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

        public async Task<TResponse<GetUserByIdResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.USER_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetUserByIdResponse>(url,
                                                                           null,
                                                                           HttpMethod.Get,
                                                                           true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetUserByIdResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetUserByIdResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateUserRequest request)
        {
            try
            {
                var url = ApiUrl.USER_UPDATE;
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
                var url = string.Format(ApiUrl.USER_DELETE,
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

        public async Task<TResponse<IEnumerable<UserDisplaySimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.USER_GET_ALL;
                var response = await HttpService.Send<IEnumerable<UserDisplaySimpleModel>>(url,
                                                                                           null,
                                                                                           HttpMethod.Get,
                                                                                           true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<UserDisplaySimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<UserDisplaySimpleModel>>(exception);
            }
        }

        public async Task<TResponse<bool>> IsBelongDefaultCrmRole()
        {
            try
            {
                var url = ApiUrl.USER_IS_DEFAULT_CRM;
                var response = await HttpService.Send<bool>(url,
                                                            null,
                                                            HttpMethod.Get,
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

        public async Task<TResponse<bool>> IsBelongDefaultInvoiceRole()
        {
            try
            {
                var url = ApiUrl.USER_IS_DEFAULT_INVOICE;
                var response = await HttpService.Send<bool>(url,
                                                            null,
                                                            HttpMethod.Get,
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

        #endregion
    }
}
