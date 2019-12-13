using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Role;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IRoleService
    {
        Task<TResponse<IEnumerable<RoleSimpleModel>>> GetAll();
    }

    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IHttpService httpService) : base(httpService)
        {
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
    }
}