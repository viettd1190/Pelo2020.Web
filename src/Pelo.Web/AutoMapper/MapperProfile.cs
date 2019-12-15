using AutoMapper;
using Pelo.Common.Dtos.User;
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
        }
    }
}
