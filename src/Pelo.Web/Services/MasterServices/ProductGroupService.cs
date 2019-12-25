using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.ProductGroup;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IProductGroupService
    {
        Task<TResponse<IEnumerable<ProductGroupSimpleModel>>> GetAll();
    }

    public class ProductGroupService : BaseService,
                                       IProductGroupService
    {
        public ProductGroupService(IHttpService httpService) : base(httpService)
        {
        }

        #region IProductGroupService Members

        public async Task<TResponse<IEnumerable<ProductGroupSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.PRODUCT_GROUP_GET_ALL;
                var response = await HttpService.Send<IEnumerable<ProductGroupSimpleModel>>(url,
                                                                                            null,
                                                                                            HttpMethod.Get,
                                                                                            true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<ProductGroupSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<ProductGroupSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
