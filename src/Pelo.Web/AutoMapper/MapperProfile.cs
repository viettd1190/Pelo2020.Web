using AutoMapper;
using Pelo.Common.Dtos.AppConfig;
using Pelo.Common.Dtos.User;
using Pelo.Web.Models.AppConfig;
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
        }
    }
}
