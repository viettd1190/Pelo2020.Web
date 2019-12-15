using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.AppConfig;
using Pelo.Common.Extensions;
using Pelo.Common.Models;
using Pelo.Web.Attributes;
using Pelo.Web.Models.AppConfig;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.MasterServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class AppConfigController : BaseController
    {
        private readonly IAppConfigService _appConfigService;

        readonly IMapper _mapper;

        public AppConfigController(IAppConfigService appConfigService,
                                   IMapper mapper,
                                   ILogger<AppConfigController> logger) : base(logger)
        {
            _appConfigService = appConfigService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _appConfigService.GetByPaging(request);
            if(result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetAppConfigPagingResponse>.Init(request.Draw));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View(new InsertAppConfigModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(InsertAppConfigModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _appConfigService.Insert(_mapper.Map<InsertAppConfigModel, InsertAppConfigRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "AppConfig");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await _appConfigService.GetById(id);
                if(model.IsSuccess)
                {
                    return View(_mapper.Map<GetAppConfigByIdResponse, AppConfigModel>(model.Data));
                }

                TempData["Update"] = model.ToJson();
                return View("Notfound");
            }
            catch (Exception exception)
            {
                TempData["Update"] = (new TResponse<bool>
                                      {
                                              Data = false,
                                              IsSuccess = false,
                                              Message = exception.ToString()
                                      }).ToJson();
                Logger.LogInformation(exception.ToString());
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppConfigModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _appConfigService.Update(_mapper.Map<AppConfigModel, UpdateAppConfigRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "AppConfig");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appConfigService.Delete(id);
            return Json(result);
        }
    }
}
