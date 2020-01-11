using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.Product;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IProductService
    {
        Task<TResponse<IEnumerable<ProductSimpleModel>>> GetAll();
    }

    public class ProductService : BaseService,
                                       IProductService
    {
        public ProductService(IHttpService httpService) : base(httpService)
        {
        }

        #region IProductService Members

        public async Task<TResponse<IEnumerable<ProductSimpleModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.PRODUCT_GET_ALL;
                var response = await HttpService.Send<IEnumerable<ProductSimpleModel>>(url,
                                                                                            null,
                                                                                            HttpMethod.Get,
                                                                                            true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<ProductSimpleModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<ProductSimpleModel>>(exception);
            }
        }

        #endregion
    }
}
