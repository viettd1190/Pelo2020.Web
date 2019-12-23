using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Customer;
using Pelo.Common.Dtos.CustomerGroup;
using Pelo.Common.Dtos.CustomerVip;
using Pelo.Web.Attributes;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CustomerServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class CustomerController : BaseController
    {
        private readonly ICustomerGroupService _customerGroupService;

        private readonly ICustomerService _customerService;

        private readonly ICustomerVipService _customerVipService;

        readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService,
                                  ICustomerGroupService customerGroupService,
                                  ICustomerVipService customerVipService,
                                  IMapper mapper,
                                  ILogger<CustomerController> logger) : base(logger)
        {
            _customerService = customerService;
            _customerVipService = customerVipService;
            _customerGroupService = customerGroupService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _customerService.GetByPaging(request);
            if(result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetCustomerPagingResponse>.Init(request.Draw));
        }

        private async Task<Tuple<IEnumerable<CustomerGroupSimpleModel>, string>> GetAllCustomerGroups()
        {
            try
            {
                var customerGroups = await _customerGroupService.GetAll();
                if(customerGroups.IsSuccess)
                    return new Tuple<IEnumerable<CustomerGroupSimpleModel>, string>(customerGroups.Data,
                                                                                    string.Empty);

                Logger.LogInformation(customerGroups.Message);
                return new Tuple<IEnumerable<CustomerGroupSimpleModel>, string>(new List<CustomerGroupSimpleModel>(),
                                                                                customerGroups.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<CustomerGroupSimpleModel>, string>(new List<CustomerGroupSimpleModel>(),
                                                                                exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<CustomerVipSimpleModel>, string>> GetAllCustomerVips()
        {
            try
            {
                var customerVips = await _customerVipService.GetAll();
                if(customerVips.IsSuccess)
                    return new Tuple<IEnumerable<CustomerVipSimpleModel>, string>(customerVips.Data,
                                                                                  string.Empty);

                Logger.LogInformation(customerVips.Message);
                return new Tuple<IEnumerable<CustomerVipSimpleModel>, string>(new List<CustomerVipSimpleModel>(),
                                                                              customerVips.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<CustomerVipSimpleModel>, string>(new List<CustomerVipSimpleModel>(),
                                                                              exception.ToString());
            }
        }

        private async Task SetViewBag()
        {
            var customerGroups = await GetAllCustomerGroups();
            ViewBag.CustomerGroups = customerGroups.Item1.ToList();
            if(!string.IsNullOrEmpty(customerGroups.Item2))
                ModelState.AddModelError("",
                                         customerGroups.Item2);
            var customerVips = await GetAllCustomerVips();
            ViewBag.CustomerVips = customerVips.Item1.ToList();
            if(!string.IsNullOrEmpty(customerVips.Item2))
                ModelState.AddModelError("",
                                         customerVips.Item2);
        }

        public async Task<IActionResult> Index()
        {
            await SetViewBag();
            return View();
        }

        //public IActionResult Add()
        //{
        //    return View(new InsertCustomerModel());
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(InsertCustomerModel model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var result = await _customerService.Insert(_mapper.Map<InsertCustomerModel, InsertCustomerRequest>(model));
        //        if(result.IsSuccess)
        //        {
        //            TempData["Update"] = result.ToJson();
        //            return RedirectToAction("Index",
        //                                    "Customer");
        //        }

        //        ModelState.AddModelError("",
        //                                 result.Message);
        //    }

        //    return View(model);
        //}

        //public async Task<IActionResult> Edit(int id)
        //{
        //    try
        //    {
        //        var model = await _customerService.GetById(id);
        //        if(model.IsSuccess)
        //        {
        //            return View(_mapper.Map<GetCustomerByIdResponse, CustomerModel>(model.Data));
        //        }

        //        TempData["Update"] = model.ToJson();
        //        return View("Notfound");
        //    }
        //    catch (Exception exception)
        //    {
        //        TempData["Update"] = (new TResponse<bool>
        //                              {
        //                                      Data = false,
        //                                      IsSuccess = false,
        //                                      Message = exception.ToString()
        //                              }).ToJson();
        //        Logger.LogInformation(exception.ToString());
        //    }

        //    return View("Notfound");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(CustomerModel model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var result = await _customerService.Update(_mapper.Map<CustomerModel, UpdateCustomerRequest>(model));
        //        if(result.IsSuccess)
        //        {
        //            TempData["Update"] = result.ToJson();
        //            return RedirectToAction("Index",
        //                                    "Customer");
        //        }

        //        ModelState.AddModelError("",
        //                                 result.Message);
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.Delete(id);
            return Json(result);
        }
    }
}
