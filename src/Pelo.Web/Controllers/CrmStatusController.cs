using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Web.Attributes;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CrmServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class CrmStatusController : BaseController
    {
        private readonly ICrmStatusService _crmStatusService;

        ILogger<CrmStatusController> _logger;

        readonly IMapper _mapper;

        public CrmStatusController(ICrmStatusService crmStatusService, ILogger<CrmStatusController> logger, IMapper mapper) : base(logger)
        {
            _crmStatusService = crmStatusService;
            _logger = logger;
            _mapper = mapper;
        }
        private async Task SetViewBag()
        {
        }
        //[HttpPost]
        //public async Task<IActionResult> GetList(DatatableRequest request)
        //{
        //    var result = await _crmStatusService.GetByPaging(request);
        //    if (result.IsSuccess) return Json(result.Data);

        //    return Json(DatatableResponse<GetCrmPagingResponse>.Init(request.Draw));
        //}

        public async Task<IActionResult> Index()
        {
            await SetViewBag();

            return View();
        }
    }
}