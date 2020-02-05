using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Branch;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IBranchService
    {
        Task<TResponse<IEnumerable<BranchSimpleModel>>> GetAll();

        Task<TResponse<DatatableResponse<GetBranchPagingResponse>>> GetByPaging(DatatableRequest request);

        Task<TResponse<bool>> Insert(InsertBranch request);

        Task<TResponse<BranchModel>> GetCrmById(int id);

        Task<TResponse<bool>> Update(UpdateBranch request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class BranchService : BaseService, IBranchService
    {
        public async Task<TResponse<IEnumerable<BranchSimpleModel>>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<BranchSimpleModel>>(ApiUrl.BRANCH_GET_ALL,
                                                                                      null,
                                                                                      HttpMethod.Get,
                                                                                      true);
                if (response.IsSuccess) return await Ok(response.Data);

                return await Fail<IEnumerable<BranchSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<BranchSimpleModel>>(exception);
            }
        }

        public async Task<TResponse<DatatableResponse<GetBranchPagingResponse>>> GetByPaging(DatatableRequest request)
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

                var response = await HttpService.Send<PageResult<GetBranchPagingResponse>>(url,
                                                                                        null,
                                                                                        HttpMethod.Get,
                                                                                        true);

                if (response.IsSuccess)
                    return await Ok(new DatatableResponse<GetBranchPagingResponse>
                    {
                        Draw = request?.Draw ?? 1,
                        RecordsFiltered = response.Data.TotalCount,
                        RecordsTotal = response.Data.TotalCount,
                        Data = response.Data.Data.ToList()
                    });

                return await Fail<DatatableResponse<GetBranchPagingResponse>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<DatatableResponse<GetBranchPagingResponse>>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertBranch request)
        {
            try
            {
                var url = ApiUrl.BRANCH_INSERT;
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

        public async Task<TResponse<BranchModel>> GetCrmById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.GET_ROLE_ID,
                                        id);
                var response = await HttpService.Send<BranchModel>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<BranchModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<BranchModel>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateBranch request)
        {
            try
            {
                var url = ApiUrl.BRANCH_UPDATE;
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

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.BRANCH_DELETE,
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

        public BranchService(IHttpService httpService) : base(httpService)
        {
        }
    }
}