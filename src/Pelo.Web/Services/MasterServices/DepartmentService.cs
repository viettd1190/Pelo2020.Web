using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Department;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IDepartmentService
    {
        Task<TResponse<IEnumerable<DepartmentSimpleModel>>> GetAll();
    }

    public class DepartmentService : BaseService,
                                     IDepartmentService
    {
        public DepartmentService(IHttpService httpService) : base(httpService)
        {
        }

        #region IDepartmentService Members

        public async Task<TResponse<IEnumerable<DepartmentSimpleModel>>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<DepartmentSimpleModel>>(ApiUrl.DEPARTMENT_GET_ALL,
                                                                                          null,
                                                                                          HttpMethod.Get,
                                                                                          true);
                if(response.IsSuccess) return await Ok(response.Data);

                return await Fail<IEnumerable<DepartmentSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<DepartmentSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
