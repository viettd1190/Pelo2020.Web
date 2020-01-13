using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Branch;
using Pelo.Common.Dtos.Crm;
using Pelo.Common.Dtos.Invoice;
using Pelo.Common.Dtos.InvoiceStatus;
using Pelo.Common.Dtos.PayMethod;
using Pelo.Common.Dtos.Product;
using Pelo.Common.Dtos.User;
using Pelo.Web.Attributes;
using Pelo.Web.Models.Crm;
using Pelo.Web.Models.Customer;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Models.Invoice;
using Pelo.Web.Services.CustomerServices;
using Pelo.Web.Services.InvoiceServices;
using Pelo.Web.Services.MasterServices;
using Pelo.Web.Services.UserServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceStatusService _invoiceStatusService;

        readonly IMapper _mapper;

        private readonly IBranchService _branchService;

        private readonly IUserService _userService;

        private IInvoiceService _invoiceService;

        private ICustomerService _customerService;

        private IProductService _productService;

        private IPayMethodService _payMethodService;

        public InvoiceController(IInvoiceStatusService invoiceStatusService,
                                 IBranchService branchService,
                                 IUserService userService,
                                 IInvoiceService invoiceService,
                                 ICustomerService customerService,
                                 IProductService productService,
                                 IPayMethodService payMethodService,
                                 IMapper mapper,
                                 ILogger<InvoiceController> logger) : base(logger)
        {
            _invoiceStatusService = invoiceStatusService;
            _branchService = branchService;
            _userService = userService;
            _invoiceService = invoiceService;
            _customerService = customerService;
            _productService = productService;
            _payMethodService = payMethodService;
            _mapper = mapper;
        }

        private async Task<Tuple<IEnumerable<UserDisplaySimpleModel>, string>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAll();
                if(users.IsSuccess)
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

        private async Task<Tuple<IEnumerable<InvoiceStatusSimpleModel>, string>> GetAllInvoiceStatuses()
        {
            try
            {
                var invoiceStatuses = await _invoiceStatusService.GetAll();
                if(invoiceStatuses.IsSuccess)
                    return new Tuple<IEnumerable<InvoiceStatusSimpleModel>, string>(invoiceStatuses.Data,
                                                                                    string.Empty);

                Logger.LogInformation(invoiceStatuses.Message);
                return new Tuple<IEnumerable<InvoiceStatusSimpleModel>, string>(new List<InvoiceStatusSimpleModel>(),
                                                                                invoiceStatuses.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<InvoiceStatusSimpleModel>, string>(new List<InvoiceStatusSimpleModel>(),
                                                                                exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<BranchSimpleModel>, string>> GetAllBranches()
        {
            try
            {
                var branches = await _branchService.GetAll();
                if(branches.IsSuccess)
                    return new Tuple<IEnumerable<BranchSimpleModel>, string>(branches.Data,
                                                                             string.Empty);

                Logger.LogInformation(branches.Message);
                return new Tuple<IEnumerable<BranchSimpleModel>, string>(new List<BranchSimpleModel>(),
                                                                         branches.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<BranchSimpleModel>, string>(new List<BranchSimpleModel>(),
                                                                         exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<ProductSimpleModel>, string>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAll();
                if (products.IsSuccess)
                    return new Tuple<IEnumerable<ProductSimpleModel>, string>(products.Data,
                                                                             string.Empty);

                Logger.LogInformation(products.Message);
                return new Tuple<IEnumerable<ProductSimpleModel>, string>(new List<ProductSimpleModel>(),
                                                                          products.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<ProductSimpleModel>, string>(new List<ProductSimpleModel>(),
                                                                         exception.ToString());
            }
        }

        private async Task<Tuple<IEnumerable<PayMethodSimpleModel>, string>> GetAllPayMethods()
        {
            try
            {
                var payMethods = await _payMethodService.GetAll();
                if (payMethods.IsSuccess)
                    return new Tuple<IEnumerable<PayMethodSimpleModel>, string>(payMethods.Data,
                                                                              string.Empty);

                Logger.LogInformation(payMethods.Message);
                return new Tuple<IEnumerable<PayMethodSimpleModel>, string>(new List<PayMethodSimpleModel>(),
                                                                          payMethods.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<PayMethodSimpleModel>, string>(new List<PayMethodSimpleModel>(),
                                                                          exception.ToString());
            }
        }

        private async Task SetViewBag()
        {
            var users = await GetAllUsers();

            var invoiceStatuses = await GetAllInvoiceStatuses();
            ViewBag.InvoiceStatuses = invoiceStatuses.Item1.ToList();
            if(!string.IsNullOrEmpty(invoiceStatuses.Item2))
                ModelState.AddModelError("",
                                         invoiceStatuses.Item2);

            var branches = await GetAllBranches();
            ViewBag.Branches = branches.Item1.ToList();
            if(!string.IsNullOrEmpty(branches.Item2))
                ModelState.AddModelError("",
                                         branches.Item2);

            List<UserDisplaySimpleModel> userCreateds = new List<UserDisplaySimpleModel>();
            var currentUser = new UserDisplaySimpleModel
                              {
                                      DisplayName = CurrentUser.DisplayName,
                                      Id = CurrentUser.Id
                              };
            var isDefaultInvoiceRole = await _userService.IsBelongDefaultInvoiceRole();
            if(isDefaultInvoiceRole.IsSuccess)
            {
                if(isDefaultInvoiceRole.Data)
                {
                    ViewBag.IsDefaultInvoiceRole = true;
                    userCreateds.AddRange(users.Item1);
                }
                else
                {
                    ViewBag.IsDefaultInvoiceRole = false;
                    ViewBag.UserId = currentUser.Id;
                }
            }
            else
            {
                ViewBag.IsDefaultInvoiceRole = false;
                ViewBag.UserId = currentUser.Id;
            }

            ViewBag.UserCreateds = userCreateds;
        }

        private async Task SetViewBag2()
        {
            var users = await GetAllUsers();
            ViewBag.Users = users.Item1.ToList();
            if(!string.IsNullOrEmpty(users.Item2))
            {
                ModelState.AddModelError("",users.Item2);
            }

            var products = await GetAllProducts();
            ViewBag.Products = products.Item1.ToList();
            if(!string.IsNullOrEmpty(products.Item2))
            {
                ModelState.AddModelError("",products.Item2);
            }

            var payMethods = await GetAllPayMethods();
            ViewBag.PayMethods = payMethods.Item1.ToList();
            if (!string.IsNullOrEmpty(payMethods.Item2))
            {
                ModelState.AddModelError("", payMethods.Item2);
            }

            var branches = await GetAllBranches();
            ViewBag.Branches = branches.Item1.ToList();
            if (!string.IsNullOrEmpty(branches.Item2))
                ModelState.AddModelError("",
                                         branches.Item2);

        }

        public async Task<IActionResult> Index()
        {
            await SetViewBag();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _invoiceService.GetByPaging(request);
            if (result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetInvoicePagingResponse>.Init(request.Draw));
        }

        public async Task<IActionResult> Add(string phone)
        {
            var customer = await _customerService.GetByPhone(phone);
            if (customer.IsSuccess)
            {
                await SetViewBag2();
                return View(new InsertInvoiceModel
                            {
                                    CustomerInfoModel = new CustomerInfoModel(customer.Data),
                                    CustomerId = customer.Data.Id,
                                    DeliveryDate = DateTime.Now
                            });
            }

            return RedirectToAction("Add",
                                    "Customer",
                                    new
                                    {
                                        nextAction = "Invoice"
                                    });
        }

        //[HttpPost]
        //public async Task<IActionResult> Add(InsertCrmModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!string.IsNullOrEmpty(model.ContactDate)
        //           && !string.IsNullOrEmpty(model.ContactTime))
        //        {
        //            DateTime contactDate = DateTime.Parse($"{model.ContactDate} {model.ContactTime}");
        //            var result = await _crmService.Insert(new InsertCrmRequest
        //            {
        //                Need = model.Need,
        //                CrmPriorityId = model.CrmPriorityId,
        //                CrmStatusId = model.CrmStatusId,
        //                CrmTypeId = model.CrmTypeId,
        //                CustomerId = model.CustomerId,
        //                CustomerSourceId = model.CustomerSourceId,
        //                Description = model.Description,
        //                ProductGroupId = model.ProductGroupId,
        //                Visit = model.Visit,
        //                ContactDate = contactDate,
        //                UserIds = model.UserIds.ToList(),
        //            });
        //            if (result.IsSuccess)
        //            {
        //                TempData["Update"] = result.ToJson();
        //                return RedirectToAction("Index",
        //                                        "Crm");
        //            }

        //            ModelState.AddModelError("",
        //                                     result.Message);
        //        }
        //    }
        //    var customer = await _customerService.GetByPhone(model.Phone);
        //    if (customer.IsSuccess)
        //    {
        //        model.CustomerInfoModel = new CustomerInfoModel(customer.Data);
        //    }
        //    await SetViewBag();
        //    return View(model);
        //}
    }
}
