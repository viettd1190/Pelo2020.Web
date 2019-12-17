using AutoMapper;
using Pelo.Common.Dtos.AppConfig;
using Pelo.Common.Dtos.CustomerGroup;
using Pelo.Common.Dtos.CustomerVip;
using Pelo.Common.Dtos.User;
using Pelo.Web.Models.AppConfig;
using Pelo.Web.Models.CustomerGroup;
using Pelo.Web.Models.CustomerVip;
using Pelo.Web.Models.User;

namespace Pelo.Web.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region User

            CreateMap<InsertUserRequest, InsertUserModel>()
                    .ReverseMap();
            CreateMap<UpdateUserRequest, UserModel>()
                    .ReverseMap();
            CreateMap<GetUserByIdResponse, UserModel>()
                    .ReverseMap();

            #endregion

            #region AppConfig

            CreateMap<InsertAppConfigRequest, InsertAppConfigModel>()
                    .ReverseMap();
            CreateMap<UpdateAppConfigRequest, AppConfigModel>()
                    .ReverseMap();
            CreateMap<GetAppConfigByIdResponse, AppConfigModel>()
                    .ReverseMap();

            #endregion

            #region CustomerGroup

            CreateMap<InsertCustomerGroupRequest, InsertCustomerGroupModel>()
                    .ReverseMap();
            CreateMap<UpdateCustomerGroupRequest, CustomerGroupModel>()
                    .ReverseMap();
            CreateMap<GetCustomerGroupByIdResponse, CustomerGroupModel>()
                    .ReverseMap();

            #endregion

            #region CustomerVip

            CreateMap<InsertCustomerVipRequest, InsertCustomerVipModel>()
                    .ReverseMap();
            CreateMap<UpdateCustomerVipRequest, CustomerVipModel>()
                    .ReverseMap();
            CreateMap<GetCustomerVipByIdResponse, CustomerVipModel>()
                    .ReverseMap();

            #endregion
        }
    }
}
