using Pelo.Web.Services.Base;
using Pelo.Web.Services.HttpServices;

namespace Pelo.Web.Services.CrmServices
{
    public interface ICrmService
    {
    }

    public class CrmService : BaseService,
        ICrmService
    {
        public CrmService(IHttpService httpService) : base(httpService)
        {
        }
    }
}