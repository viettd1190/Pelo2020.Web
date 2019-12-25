using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CrmStatus;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CrmServices
{
    public interface ICrmStatusService
    {
        Task<TResponse<IEnumerable<CrmStatusSimpleModel>>> GetAll();
    }

    public class CrmStatusService : BaseService,
                                    ICrmStatusService
    {
        public CrmStatusService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICrmStatusService Members

        public async Task<TResponse<IEnumerable<CrmStatusSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.CRM_STATUS_GET_ALL;
                var response = await HttpService.Send<IEnumerable<CrmStatusSimpleModel>>(url,
                                                                                         null,
                                                                                         HttpMethod.Get,
                                                                                         true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<CrmStatusSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<CrmStatusSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
