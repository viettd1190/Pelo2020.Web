using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Branch;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IBranchService
    {
        Task<TResponse<IEnumerable<BranchSimpleModel>>> GetAll();
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

        public BranchService(IHttpService httpService) : base(httpService)
        {
        }
    }
}