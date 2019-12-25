using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.CrmType;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CrmServices
{
    public interface ICrmTypeService
    {
        Task<TResponse<IEnumerable<CrmTypeSimpleModel>>> GetAll();
    }

    public class CrmTypeService : BaseService,
                                  ICrmTypeService
    {
        public CrmTypeService(IHttpService httpService) : base(httpService)
        {
        }

        #region ICrmTypeService Members

        public async Task<TResponse<IEnumerable<CrmTypeSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.CRM_TYPE_GET_ALL;
                var response = await HttpService.Send<IEnumerable<CrmTypeSimpleModel>>(url,
                                                                                       null,
                                                                                       HttpMethod.Get,
                                                                                       true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<CrmTypeSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<CrmTypeSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
