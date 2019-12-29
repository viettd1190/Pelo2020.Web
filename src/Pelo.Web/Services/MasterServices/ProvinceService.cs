using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.District;
using Pelo.Common.Dtos.Province;
using Pelo.Common.Dtos.Ward;
using Pelo.Common.Models;
using Pelo.Web.Commons;
using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.MasterServices
{
    public interface IProvinceService
    {
        Task<TResponse<IEnumerable<ProvinceModel>>> GetAllProvinces();

        Task<TResponse<IEnumerable<DistrictModel>>> GetAllDistricts(int provinceId);

        Task<TResponse<IEnumerable<WardModel>>> GetAllWards(int districtId);
    }

    public class ProvinceService : BaseService,
                                   IProvinceService
    {
        public ProvinceService(IHttpService httpService) : base(httpService)
        {
        }

        #region IProvinceService Members

        public async Task<TResponse<IEnumerable<ProvinceModel>>> GetAllProvinces()
        {
            try
            {
                var url = ApiUrl.PROVINCE_GET_ALL;
                var response = await HttpService.Send<IEnumerable<ProvinceModel>>(url,
                                                                                  null,
                                                                                  HttpMethod.Get,
                                                                                  true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<ProvinceModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<ProvinceModel>>(exception);
            }
        }

        public async Task<TResponse<IEnumerable<DistrictModel>>> GetAllDistricts(int provinceId)
        {
            try
            {
                var url = string.Format(ApiUrl.DISTRICT_GET_ALL,
                                        provinceId);
                var response = await HttpService.Send<IEnumerable<DistrictModel>>(url,
                                                                                  null,
                                                                                  HttpMethod.Get,
                                                                                  true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<DistrictModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<DistrictModel>>(exception);
            }
        }

        public async Task<TResponse<IEnumerable<WardModel>>> GetAllWards(int districtId)
        {
            try
            {
                var url = string.Format(ApiUrl.WARD_GET_ALL,
                                        districtId);
                var response = await HttpService.Send<IEnumerable<WardModel>>(url,
                                                                              null,
                                                                              HttpMethod.Get,
                                                                              true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<WardModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<WardModel>>(exception);
            }
        }

        #endregion
    }
}
