using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Customer;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<TResponse<DatatableResponse<GetCustomerPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertCustomerRequest request);

        Task<TResponse<GetCustomerByIdResponse>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateCustomerRequest request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class CustomerService : BaseService,
                                   ICustomerService
    {
        public CustomerService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICustomerService Members

        public async Task<TResponse<DatatableResponse<GetCustomerPagingResponse>>> GetByPaging(DatatableRequest request)
        {
            try
            {
                var code = string.Empty;
                var name = string.Empty;
                var provinceId = 0;
                var districtId = 0;
                var wardId = 0;
                var address = string.Empty;
                var phone = string.Empty;
                var email = string.Empty;
                var customerGroupId = 0;
                var customerVipId = 0;
                if(request?.Columns != null)
                    if(request.Columns.Any())
                    {
                        code = request.Columns[0]
                                      .Search?.Value ?? string.Empty;
                        name = request.Columns[1]
                                      .Search?.Value ?? string.Empty;
                        var province = request.Columns[2]
                                                 .Search?.Value ?? string.Empty;
                        var district = request.Columns[3]
                                                 .Search?.Value ?? string.Empty;
                        var ward = request.Columns[4]
                                             .Search?.Value ?? string.Empty;
                        address = request.Columns[5]
                                         .Search?.Value ?? string.Empty;
                        phone = request.Columns[6]
                                       .Search?.Value ?? string.Empty;
                        email = request.Columns[7]
                                       .Search?.Value ?? string.Empty;
                        var customerGroup = request.Columns[8]
                                                      .Search?.Value ?? string.Empty;
                        var customerVip = request.Columns[9]
                                                    .Search?.Value ?? string.Empty;

                        if(!string.IsNullOrEmpty(province))
                        {
                            int.TryParse(province,
                                         out provinceId);
                        }

                        if(!string.IsNullOrEmpty(district))
                        {
                            int.TryParse(district,
                                         out districtId);
                        }

                        if(!string.IsNullOrEmpty(ward))
                        {
                            int.TryParse(ward,
                                         out wardId);
                        }

                        if(!string.IsNullOrEmpty(customerGroup))
                        {
                            int.TryParse(customerGroup,
                                         out customerGroupId);
                        }

                        if(!string.IsNullOrEmpty(customerVip))
                        {
                            int.TryParse(customerVip,
                                         out customerVipId);
                        }
                    }

                var start = 1;

                if(request != null) start = request.Start / request.Length + 1;

                var columnOrder = "date_updated";
                var sortDir = "DESC";

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

                var url = string.Format(ApiUrl.CUSTOMER_GET_BY_PAGING,
                                        code,
                                        name,
                                        provinceId,
                                        districtId,
                                        wardId,
                                        address,
                                        phone,
                                        email,
                                        customerGroupId,
                                        customerVipId,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetCustomerPagingResponse>>(url,
                                                                                             null,
                                                                                             HttpMethod.Get,
                                                                                             true);

                if(response.IsSuccess)
                    return await Ok(new DatatableResponse<GetCustomerPagingResponse>
                                    {
                                            Draw = request?.Draw ?? 1,
                                            RecordsFiltered = response.Data.TotalCount,
                                            RecordsTotal = response.Data.TotalCount,
                                            Data = response.Data.Data.ToList()
                                    });

                return await Fail<DatatableResponse<GetCustomerPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetCustomerPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertCustomerRequest request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_INSERT;
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

        public async Task<TResponse<GetCustomerByIdResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetCustomerByIdResponse>(url,
                                                                               null,
                                                                               HttpMethod.Get,
                                                                               true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCustomerByIdResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCustomerByIdResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateCustomerRequest request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_UPDATE;
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
                var url = string.Format(ApiUrl.CUSTOMER_DELETE,
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
