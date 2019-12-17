using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CustomerGroup;
using Pelo.Common.Extensions;
using Pelo.Common.Models;
using Pelo.Web.Attributes;
using Pelo.Web.Models.CustomerGroup;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CustomerServices;
using Pelo.Web.Services.MasterServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class CustomerGroupController : BaseController
    {
        private readonly ICustomerGroupService _customerGroupService;

        readonly IMapper _mapper;

        public CustomerGroupController(ICustomerGroupService customerGroupService,
                                   IMapper mapper,
                                   ILogger<CustomerGroupController> logger) : base(logger)
        {
            _customerGroupService = customerGroupService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _customerGroupService.GetByPaging(request);
            if(result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetCustomerGroupPagingResponse>.Init(request.Draw));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View(new InsertCustomerGroupModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(InsertCustomerGroupModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _customerGroupService.Insert(_mapper.Map<InsertCustomerGroupModel, InsertCustomerGroupRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "CustomerGroup");
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
                var model = await _customerGroupService.GetById(id);
                if(model.IsSuccess)
                {
                    return View(_mapper.Map<GetCustomerGroupByIdResponse, CustomerGroupModel>(model.Data));
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
        public async Task<IActionResult> Edit(CustomerGroupModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _customerGroupService.Update(_mapper.Map<CustomerGroupModel, UpdateCustomerGroupRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "CustomerGroup");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerGroupService.Delete(id);
            return Json(result);
        }
    }
}
