using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CrmPriority;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CrmServices
{
    public interface ICrmPriorityService
    {
        Task<TResponse<IEnumerable<CrmPrioritySimpleModel>>> GetAll();
    }

    public class CrmPriorityService : BaseService,
                                      ICrmPriorityService
    {
        public CrmPriorityService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICrmPriorityService Members

        public async Task<TResponse<IEnumerable<CrmPrioritySimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.CRM_PRIORITY_GET_ALL;
                var response = await HttpService.Send<IEnumerable<CrmPrioritySimpleModel>>(url,
                                                                                           null,
                                                                                           HttpMethod.Get,
                                                                                           true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<CrmPrioritySimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<CrmPrioritySimpleModel>>(exception);
            }
        }

        #endregion
    }
}
