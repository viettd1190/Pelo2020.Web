using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CrmStatus;
using Pelo.Web.Models.CrmStatus;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CrmServices;
using Pelo.Common.Extensions;

namespace Pelo.Web.Controllers
{
    public class CrmStatusController : BaseController
    {
        ICrmStatusService _crmStatusService;
        readonly IMapper _mapper;
        public CrmStatusController(ILogger<BaseController> logger, ICrmStatusService crmStatusService, IMapper mapper) : base(logger)
        {
            _crmStatusService = crmStatusService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _crmStatusService.GetByPaging(request);
            if (result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetCrmStatusPagingResponse>.Init(request.Draw));
        }
        public IActionResult Add()
        {
            return View(new CrmStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CrmStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _crmStatusService.Insert(new InsertCrmStatus
                {
                    Name = model.Name,
                    Color = model.Color,
                    IsSendSms = model.IsSendSms,
                    SmsContent = model.SmsContent
                });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "CrmStatus");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Update(int id)
        {
            var model = await _crmStatusService.GetCrmById(id);
            if (model.IsSuccess)
            {
                if (model.Data != null)
                {
                    return View(new CrmStatusModel
                    {
                        Id= model.Data.Id,
                        Name = model.Data.Name,
                        Color = model.Data.Color,
                        SmsContent = model.Data.SmsContent,
                        IsSendSms = model.Data.IsSendSms
                    });
                }
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CrmStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _crmStatusService.Update(new UpdateCrmStatus
                {
                    Id = model.Id,
                    Color = model.Color,
                    Name = model.Name,
                    IsSendSms = model.IsSendSms,
                    SmsContent = model.SmsContent                    
                });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "CrmStatus");
                }
                else
                {
                    ModelState.AddModelError("",
                                         result.Message);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crmStatusService.Delete(id);
            return Json(result);
        }
    }
}