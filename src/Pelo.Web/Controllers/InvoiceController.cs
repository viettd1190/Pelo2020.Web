using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Branch;
using Pelo.Common.Dtos.InvoiceStatus;
using Pelo.Common.Dtos.User;
using Pelo.Web.Attributes;
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

        public InvoiceController(IInvoiceStatusService invoiceStatusService,
                                 IBranchService branchService,
                                 IUserService userService,
                                 IMapper mapper,
                                 ILogger<InvoiceController> logger) : base(logger)
        {
            _invoiceStatusService = invoiceStatusService;
            _branchService = branchService;
            _userService = userService;
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

        public async Task<IActionResult> Index()
        {
            await SetViewBag();

            return View();
        }

    }
}
