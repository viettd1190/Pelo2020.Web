using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CustomerVip;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CustomerServices
{
    public interface ICustomerVipService
    {
        Task<TResponse<DatatableResponse<GetCustomerVipPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertCustomerVipRequest request);

        Task<TResponse<GetCustomerVipByIdResponse>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateCustomerVipRequest request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<IEnumerable<CustomerVipSimpleModel>>> GetAll();
    }

    public class CustomerVipService : BaseService,
                                      ICustomerVipService
    {
        public CustomerVipService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICustomerVipService Members

        public async Task<TResponse<DatatableResponse<GetCustomerVipPagingResponse>>> GetByPaging(DatatableRequest request)
        {
            try
            {
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

                var url = string.Format(ApiUrl.CUSTOMER_VIP_GET_BY_PAGING,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetCustomerVipPagingResponse>>(url,
                                                                                                null,
                                                                                                HttpMethod.Get,
                                                                                                true);

                if(response.IsSuccess)
                    return await Ok(new DatatableResponse<GetCustomerVipPagingResponse>
                                    {
                                            Draw = request?.Draw ?? 1,
                                            RecordsFiltered = response.Data.TotalCount,
                                            RecordsTotal = response.Data.TotalCount,
                                            Data = response.Data.Data.ToList()
                                    });

                return await Fail<DatatableResponse<GetCustomerVipPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetCustomerVipPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertCustomerVipRequest request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_VIP_INSERT;
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

        public async Task<TResponse<GetCustomerVipByIdResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_VIP_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetCustomerVipByIdResponse>(url,
                                                                                  null,
                                                                                  HttpMethod.Get,
                                                                                  true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCustomerVipByIdResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCustomerVipByIdResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateCustomerVipRequest request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_VIP_UPDATE;
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
                var url = string.Format(ApiUrl.CUSTOMER_VIP_DELETE,
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

        public async Task<TResponse<IEnumerable<CustomerVipSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.CUSTOMER_VIP_GET_ALL;
                var response = await HttpService.Send<IEnumerable<CustomerVipSimpleModel>>(url,
                                                                                           null,
                                                                                           HttpMethod.Get,
                                                                                           true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<CustomerVipSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<CustomerVipSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
