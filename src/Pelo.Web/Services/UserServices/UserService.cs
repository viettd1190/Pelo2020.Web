using System;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Account;
using Pelo.Common.Dtos.User;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.UserServices
{
    public interface IUserService
    {
        Task<TResponse<PageResult<GetUserPagingResponse>>> GetByPaging(GetUserPagingRequest request);
    }

    public class UserService : BaseService,
        IUserService
    {
        public UserService(IHttpService httpService) : base(httpService)
        {
        }

        #region IUserService Members

        public async Task<TResponse<PageResult<GetUserPagingResponse>>> GetByPaging(GetUserPagingRequest request)
        {
            try
            {
                string url = string.Format(ApiUrl.USER_GET_BY_PAGING, request.Username, request.DisplayName,
                    request.FullName, request.PhoneNumber, request.BranchId, request.RoleId, request.Page,
                    request.PageSize, request.ColumnOrder, request.SortDir);

                return await Execute<PageResult<GetUserPagingResponse>>(url,
                    null,
                    HttpMethod.Get);
            }
            catch (Exception exception)
            {
                return await Fail<PageResult<GetUserPagingResponse>>(exception);
            }
        }

        #endregion
    }
}