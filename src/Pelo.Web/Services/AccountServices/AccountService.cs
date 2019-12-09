using System;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Account;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.AccountServices
{
    public interface IAccountService
    {
        Task<TResponse<LogonResponse>> LogOn(string username,
            string password);
    }

    public class AccountService : BaseService,
        IAccountService
    {
        public AccountService(IHttpService httpService) : base(httpService)
        {
        }

        #region IAccountService Members

        public async Task<TResponse<LogonResponse>> LogOn(string username,
            string password)
        {
            try
            {
                return await Execute<LogonResponse>(ApiUrl.LOG_ON,
                    new
                    {
                        username,
                        password
                    },
                    HttpMethod.Post);
            }
            catch (Exception exception)
            {
                return await Fail<LogonResponse>(exception);
            }
        }

        #endregion
    }
}