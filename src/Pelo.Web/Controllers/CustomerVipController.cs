using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CustomerVip;
using Pelo.Common.Extensions;
using Pelo.Common.Models;
using Pelo.Web.Attributes;
using Pelo.Web.Models.CustomerVip;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CustomerServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class CustomerVipController : BaseController
    {
        private readonly ICustomerVipService _customerVipService;

        readonly IMapper _mapper;

        public CustomerVipController(ICustomerVipService customerVipService,
                                   IMapper mapper,
                                   ILogger<CustomerVipController> logger) : base(logger)
        {
            _customerVipService = customerVipService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _customerVipService.GetByPaging(request);
            if(result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetCustomerVipPagingResponse>.Init(request.Draw));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View(new InsertCustomerVipModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(InsertCustomerVipModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _customerVipService.Insert(_mapper.Map<InsertCustomerVipModel, InsertCustomerVipRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "CustomerVip");
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
                var model = await _customerVipService.GetById(id);
                if(model.IsSuccess)
                {
                    return View(_mapper.Map<GetCustomerVipByIdResponse, CustomerVipModel>(model.Data));
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
        public async Task<IActionResult> Edit(CustomerVipModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _customerVipService.Update(_mapper.Map<CustomerVipModel, UpdateCustomerVipRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "CustomerVip");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerVipService.Delete(id);
            return Json(result);
        }
    }
}
