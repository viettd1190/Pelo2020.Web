using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CrmPriority;
using Pelo.Web.Models.CrmPriority;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CrmServices;
using Pelo.Common.Extensions;

namespace Pelo.Web.Controllers
{
    public class CrmPriorityController : BaseController
    {
        ICrmPriorityService _crmPriorityService;
        readonly IMapper _mapper;
        public CrmPriorityController(ILogger<BaseController> logger, ICrmPriorityService crmPriorityService, IMapper mapper) : base(logger)
        {
            _crmPriorityService = crmPriorityService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _crmPriorityService.GetByPaging(request);
            if (result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetCrmPriorityPagingResponse>.Init(request.Draw));
        }
        public IActionResult Add()
        {
            return View(new CrmPriorityModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CrmPriorityModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _crmPriorityService.Insert(new InsertCrmPriority
                {
                    Name = model.Name,
                    Color = model.Color,
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
            var model = await _crmPriorityService.GetCrmById(id);
            if (model.IsSuccess)
            {
                if (model.Data != null)
                {
                    return View(new CrmPriorityModel
                    {
                        Id = model.Data.Id,
                        Name = model.Data.Name,
                        Color = model.Data.Color,
                    });
                }
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CrmPriorityModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _crmPriorityService.Update(new UpdateCrmPriority
                {
                    Id = model.Id,
                    Color = model.Color,
                    Name = model.Name,
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
            var result = await _crmPriorityService.Delete(id);
            return Json(result);
        }
    }
}