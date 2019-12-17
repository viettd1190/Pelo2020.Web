using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CustomerGroup;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CustomerServices
{
    public interface ICustomerGroupService
    {
        Task<TResponse<DatatableResponse<GetCustomerGroupPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertCustomerGroupRequest request);

        Task<TResponse<GetCustomerGroupByIdResponse>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateCustomerGroupRequest request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class CustomerGroupService : BaseService,
                                        ICustomerGroupService
    {
        public CustomerGroupService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICustomerGroupService Members

        public async Task<TResponse<DatatableResponse<GetCustomerGroupPagingResponse>>> GetByPaging(DatatableRequest request)
        {
            try
            {
                var name = string.Empty;
                if(request?.Columns != null)
                    if(request.Columns.Any())
                    {
                        name = request.Columns[0]
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

                var url = string.Format(ApiUrl.CUSTOMER_GROUP_GET_BY_PAGING,
                                        name,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetCustomerGroupPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                if(response.IsSuccess)
                    return await Ok(new DatatableResponse<GetCustomerGroupPagingResponse>
                                    {
                                            Draw = request?.Draw ?? 1,
                                            RecordsFiltered = response.Data.TotalCount,
                                            RecordsTotal = response.Data.TotalCount,
                                            Data = response.Data.Data.ToList()
                                    });

                return await Fail<DatatableResponse<GetCustomerGroupPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetCustomerGroupPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertCustomerGroupRequest request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_GROUP_INSERT;
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

        public async Task<TResponse<GetCustomerGroupByIdResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_GROUP_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetCustomerGroupByIdResponse>(url,
                                                                                    null,
                                                                                    HttpMethod.Get,
                                                                                    true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCustomerGroupByIdResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCustomerGroupByIdResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateCustomerGroupRequest request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_GROUP_UPDATE;
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
                var url = string.Format(ApiUrl.CUSTOMER_GROUP_DELETE,
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
