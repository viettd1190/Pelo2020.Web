using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Crm;
using Pelo.Common.Dtos.CrmPriority;
using Pelo.Common.Dtos.CrmStatus;
using Pelo.Common.Dtos.CrmType;
using Pelo.Common.Dtos.Customer;
using Pelo.Common.Dtos.CustomerGroup;
using Pelo.Common.Dtos.CustomerSource;
using Pelo.Common.Dtos.CustomerVip;
using Pelo.Common.Dtos.ProductGroup;
using Pelo.Common.Dtos.User;
using Pelo.Common.Extensions;
using Pelo.Web.Attributes;
using Pelo.Web.Models.Crm;
using Pelo.Web.Models.Customer;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CrmServices;
using Pelo.Web.Services.CustomerServices;
using Pelo.Web.Services.MasterServices;
using Pelo.Web.Services.UserServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class CrmController : BaseController
    {
        private readonly ICrmPriorityService _crmPriorityService;

        private readonly ICrmService _crmService;

        private readonly ICrmStatusService _crmStatusService;

        private readonly ICrmTypeService _crmTypeService;

        private readonly ICustomerSourceService _customerSourceService;

        readonly IMapper _mapper;

        private readonly IProductGroupService _productGroupService;

        private readonly ICustomerGroupService _customerGroupService;

        private readonly ICustomerVipService _customerVipService;

        private readonly IProvinceService _provinceService;

        private readonly ICustomerService _customerService;

        private IUserService _userService;

        public CrmController(ICrmService crmService,
                             ICrmTypeService crmTypeService,
                             ICrmStatusService crmStatusService,
                             ICrmPriorityService crmPriorityService,
                             ICustomerSourceService customerSourceService,
                             IProductGroupService productGroupService,
                             ICustomerGroupService customerGroupService,
                             ICustomerVipService customerVipService,
                             IProvinceService provinceService,
                             IUserService userService,
                             ICustomerService customerService,
                             IMapper mapper,
                             ILogger<CrmController> logger) : base(logger)
        {
            _crmService = crmService;
            _crmTypeService = crmTypeService;
            _crmStatusService = crmStatusService;
            _crmPriorityService = crmPriorityService;
            _customerSourceService = customerSourceService;
            _productGroupService = productGroupService;
            _customerGroupService = customerGroupService;
            _customerVipService = customerVipService;
            _provinceService = provinceService;
            _userService = userService;
            _mapper = mapper;
            _customerService = customerService;
        }

        private async Task<Tuple<IEnumerable<UserDisplaySimpleModel>, string>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAll();
                if (users.IsSuccess)
                    return new Tuple<IEnumerable<UserDisplaySimpleModel>, string>(users.Data,
                                                                                    string.Empty);

                Logger.LogInformation(users.Message);
                return new Tuple<IEnumerable<UserDisplaySimpleModel>, string>(new List<UserDisplaySimpleModel>(),
                                                                                users.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<UserDisplaySimpleModel>, string>(new List<UserDisplaySimpleModel>(),
                                                                                exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<CustomerGroupSimpleModel>, string>> GetAllCustomerGroups()
        {
            try
            {
                var customerGroups = await _customerGroupService.GetAll();
                if (customerGroups.IsSuccess)
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
                if (customerVips.IsSuccess)
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

        private async Task<Tuple<IEnumerable<CustomerSourceSimpleModel>, string>> GetAllCustomerSources()
        {
            try
            {
                var customerSources = await _customerSourceService.GetAll();
                if (customerSources.IsSuccess)
                    return new Tuple<IEnumerable<CustomerSourceSimpleModel>, string>(customerSources.Data,
                                                                                     string.Empty);

                Logger.LogInformation(customerSources.Message);
                return new Tuple<IEnumerable<CustomerSourceSimpleModel>, string>(new List<CustomerSourceSimpleModel>(),
                                                                                 customerSources.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<CustomerSourceSimpleModel>, string>(new List<CustomerSourceSimpleModel>(),
                                                                                 exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<CrmStatusSimpleModel>, string>> GetAllCrmStatuses()
        {
            try
            {
                var crmStatuses = await _crmStatusService.GetAll();
                if (crmStatuses.IsSuccess)
                    return new Tuple<IEnumerable<CrmStatusSimpleModel>, string>(crmStatuses.Data,
                                                                                string.Empty);

                Logger.LogInformation(crmStatuses.Message);
                return new Tuple<IEnumerable<CrmStatusSimpleModel>, string>(new List<CrmStatusSimpleModel>(),
                                                                            crmStatuses.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<CrmStatusSimpleModel>, string>(new List<CrmStatusSimpleModel>(),
                                                                            exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<ProductGroupSimpleModel>, string>> GetAllProductGroups()
        {
            try
            {
                var productGroupes = await _productGroupService.GetAll();
                if (productGroupes.IsSuccess)
                    return new Tuple<IEnumerable<ProductGroupSimpleModel>, string>(productGroupes.Data,
                                                                                   string.Empty);

                Logger.LogInformation(productGroupes.Message);
                return new Tuple<IEnumerable<ProductGroupSimpleModel>, string>(new List<ProductGroupSimpleModel>(),
                                                                               productGroupes.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<ProductGroupSimpleModel>, string>(new List<ProductGroupSimpleModel>(),
                                                                               exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<CrmTypeSimpleModel>, string>> GetAllCrmTypes()
        {
            try
            {
                var crmTypees = await _crmTypeService.GetAll();
                if (crmTypees.IsSuccess)
                    return new Tuple<IEnumerable<CrmTypeSimpleModel>, string>(crmTypees.Data,
                                                                              string.Empty);

                Logger.LogInformation(crmTypees.Message);
                return new Tuple<IEnumerable<CrmTypeSimpleModel>, string>(new List<CrmTypeSimpleModel>(),
                                                                          crmTypees.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<CrmTypeSimpleModel>, string>(new List<CrmTypeSimpleModel>(),
                                                                          exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<CrmPrioritySimpleModel>, string>> GetAllCrmPriorities()
        {
            try
            {
                var crmPriorityes = await _crmPriorityService.GetAll();
                if (crmPriorityes.IsSuccess)
                    return new Tuple<IEnumerable<CrmPrioritySimpleModel>, string>(crmPriorityes.Data,
                                                                                  string.Empty);

                Logger.LogInformation(crmPriorityes.Message);
                return new Tuple<IEnumerable<CrmPrioritySimpleModel>, string>(new List<CrmPrioritySimpleModel>(),
                                                                              crmPriorityes.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<CrmPrioritySimpleModel>, string>(new List<CrmPrioritySimpleModel>(),
                                                                              exception.ToString());
            }
        }

        private async Task SetViewBag()
        {
            var userCares = await GetAllUsers();
            ViewBag.UserCares = userCares.Item1.ToList();
            if (!string.IsNullOrEmpty(userCares.Item2))
            {
                ModelState.AddModelError("",
                                         userCares.Item2);
            }

            var customerSources = await GetAllCustomerSources();
            ViewBag.CustomerSources = customerSources.Item1.ToList();
            if (!string.IsNullOrEmpty(customerSources.Item2))
                ModelState.AddModelError("",
                                         customerSources.Item2);

            var customerGroups = await GetAllCustomerGroups();
            ViewBag.CustomerGroups = customerGroups.Item1.ToList();
            if (!string.IsNullOrEmpty(customerGroups.Item2))
                ModelState.AddModelError("",
                                         customerGroups.Item2);

            var customerVips = await GetAllCustomerVips();
            ViewBag.CustomerVips = customerVips.Item1.ToList();
            if (!string.IsNullOrEmpty(customerVips.Item2))
                ModelState.AddModelError("",
                                         customerVips.Item2);

            var productGroups = await GetAllProductGroups();
            ViewBag.ProductGroups = productGroups.Item1.ToList();
            if (!string.IsNullOrEmpty(productGroups.Item2))
                ModelState.AddModelError("",
                                         productGroups.Item2);

            var crmTypes = await GetAllCrmTypes();
            ViewBag.CrmTypes = crmTypes.Item1.ToList();
            if (!string.IsNullOrEmpty(crmTypes.Item2))
                ModelState.AddModelError("",
                                         crmTypes.Item2);

            var crmPriorities = await GetAllCrmPriorities();
            ViewBag.CrmPriorities = crmPriorities.Item1.ToList();
            if (!string.IsNullOrEmpty(crmPriorities.Item2))
                ModelState.AddModelError("",
                                         crmPriorities.Item2);

            var crmStatuses = await GetAllCrmStatuses();
            ViewBag.CrmStatuses = crmStatuses.Item1.ToList();
            if (!string.IsNullOrEmpty(crmStatuses.Item2))
                ModelState.AddModelError("",
                                         crmStatuses.Item2);

            List<UserDisplaySimpleModel> userCreateds = new List<UserDisplaySimpleModel>();
            var currentUser = new UserDisplaySimpleModel
            {
                DisplayName = CurrentUser.DisplayName,
                Id = CurrentUser.Id
            };
            var isDefaultCrmRole = await _userService.IsBelongDefaultCrmRole();
            if (isDefaultCrmRole.IsSuccess)
            {
                if (isDefaultCrmRole.Data)
                {
                    ViewBag.IsDefaultCrmRole = true;
                    userCreateds.AddRange(userCares.Item1);
                }
                else
                {
                    ViewBag.IsDefaultCrmRole = false;
                    ViewBag.UserId = currentUser.Id;
                    //userCreateds.Add(currentUser);
                }
            }
            else
            {
                ViewBag.IsDefaultCrmRole = false;
                ViewBag.UserId = currentUser.Id;
                //userCreateds.Add(currentUser);
            }

            ViewBag.UserCreateds = userCreateds;
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _crmService.GetByPaging(request);
            if (result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetCrmPagingResponse>.Init(request.Draw));
        }

        public async Task<IActionResult> Index()
        {
            await SetViewBag();

            return View();
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public async Task<IActionResult> GetAllProvinces()
        {
            var provinces = await _provinceService.GetAllProvinces();
            if (provinces.IsSuccess)
            {
                return Json(provinces.Data);
            }

            return null;
        }

        [HttpPost]
        [OutputCache(Duration = 86400,
                VaryByParam = "id")]
        public async Task<IActionResult> GetAllDistricts(int id)
        {
            var districts = await _provinceService.GetAllDistricts(id);
            if (districts.IsSuccess)
            {
                return Json(districts.Data);
            }

            return null;
        }

        [HttpPost]
        [OutputCache(Duration = 86400,
                VaryByParam = "id")]
        public async Task<IActionResult> GetAllWards(int id)
        {
            var wards = await _provinceService.GetAllWards(id);
            if (wards.IsSuccess)
            {
                return Json(wards.Data);
            }

            return null;
        }

        public async Task<IActionResult> Add(string phone)
        {
            var customer = await _customerService.GetByPhone(phone);
            if (customer.IsSuccess)
            {
                await SetViewBag();
                return View(new InsertCrmModel
                {
                    CustomerInfoModel = new CustomerInfoModel(customer.Data),
                    ContactDate = string.Empty,
                    ContactTime = string.Empty,
                    Need = string.Empty,
                    Description = string.Empty,
                    Visit = -1,
                    UserIds = null,
                    CustomerId = customer.Data.Id,
                });
            }
            else
            {
                return RedirectToAction("Add", "Customer", new { nextAction = "Crm"});
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(InsertCrmModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.ContactDate) && !string.IsNullOrEmpty(model.ContactTime))
                {
                    DateTime contactDate = DateTime.Parse($"{model.ContactDate} {model.ContactTime}");
                    var result = _crmService.Insert(new InsertCrmRequest
                    {
                        Need = model.Need,
                        CrmPriorityId = model.CrmPriorityId,
                        CrmStatusId = model.CrmStatusId,
                        CrmTypeId = model.CrmTypeId,
                        CustomerId = model.CustomerId,
                        CustomerSourceId = model.CustomerSourceId,
                        Description = model.Description,
                        ProductGroupId = model.ProductGroupId,
                        Visit = model.Visit,
                        ContactDate = contactDate,
                        UserIds = Util.GetArrays(model.UserIds),
                        Code = model.Code
                    });
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Info(string phone)
        {
            var customer = await _customerService.GetByPhone(phone);
            if (customer.IsSuccess)
            {
                return View(new CustomerInfoModel(customer.Data));
            }
            else
            {
                return RedirectToAction("Add", "Customer", new { nextAction = "Crm" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetListByCustomer(DatatableRequest request)
        {
            var result = await _crmService.GetCustomerCrmByPaging(request);
            if (result.IsSuccess) return Json(result.Data);
            return Json(DatatableResponse<GetCrmPagingResponse>.Init(request.Draw));
        }
    }
}
