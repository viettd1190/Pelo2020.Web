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
using Pelo.Common.Dtos.CustomerSource;
using Pelo.Common.Dtos.ProductGroup;
using Pelo.Web.Attributes;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.CrmServices;
using Pelo.Web.Services.CustomerServices;
using Pelo.Web.Services.MasterServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class CrmController : BaseController
    {
        readonly IMapper _mapper;

        private readonly ICrmPriorityService _crmPriorityService;

        private readonly ICrmService _crmService;

        private readonly ICrmStatusService _crmStatusService;

        private readonly ICrmTypeService _crmTypeService;

        private readonly ICustomerSourceService _customerSourceService;

        private readonly IProductGroupService _productGroupService;

        public CrmController(ICrmService crmService,
                             ICrmTypeService crmTypeService,
                             ICrmStatusService crmStatusService,
                             ICrmPriorityService crmPriorityService,
                             ICustomerSourceService customerSourceService,
                             IProductGroupService productGroupService,
                             IMapper mapper,
                             ILogger<CrmController> logger) : base(logger)
        {
            _crmService = crmService;
            _crmTypeService = crmTypeService;
            _crmStatusService = crmStatusService;
            _crmPriorityService = crmPriorityService;
            _customerSourceService = customerSourceService;
            _productGroupService = productGroupService;
            _mapper = mapper;
        }

        private async Task<Tuple<IEnumerable<CustomerSourceSimpleModel>, string>> GetAllCustomerSources()
        {
            try
            {
                var customerSources = await _customerSourceService.GetAll();
                if(customerSources.IsSuccess)
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
                if(crmStatuses.IsSuccess)
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
                if(productGroupes.IsSuccess)
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
                if(crmTypees.IsSuccess)
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
                if(crmPriorityes.IsSuccess)
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
            var customerSources = await GetAllCustomerSources();
            ViewBag.Customersources = customerSources.Item1.ToList();
            if(!string.IsNullOrEmpty(customerSources.Item2))
                ModelState.AddModelError("",
                                         customerSources.Item2);

            var productGroups = await GetAllProductGroups();
            ViewBag.ProductGroups = productGroups.Item1.ToList();
            if(!string.IsNullOrEmpty(productGroups.Item2))
                ModelState.AddModelError("",
                                         productGroups.Item2);

            var crmTypes = await GetAllCrmTypes();
            ViewBag.CrmTypes = crmTypes.Item1.ToList();
            if(!string.IsNullOrEmpty(crmTypes.Item2))
                ModelState.AddModelError("",
                                         crmTypes.Item2);

            var crmPriorities = await GetAllCrmPriorities();
            ViewBag.CrmPriorities = crmPriorities.Item1.ToList();
            if(!string.IsNullOrEmpty(crmPriorities.Item2))
                ModelState.AddModelError("",
                                         crmPriorities.Item2);

            var crmStatuses = await GetAllCrmStatuses();
            ViewBag.CrmStatuses = crmStatuses.Item1.ToList();
            if(!string.IsNullOrEmpty(crmStatuses.Item2))
                ModelState.AddModelError("",
                                         crmStatuses.Item2);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _crmService.GetByPaging(request);
            if(result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetCrmPagingResponse>.Init(request.Draw));
        }

        public async Task<IActionResult> Index()
        {
            await SetViewBag();

            return View();
        }
    }
}
