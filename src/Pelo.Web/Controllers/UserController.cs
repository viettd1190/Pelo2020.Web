using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Branch;
using Pelo.Common.Dtos.Department;
using Pelo.Common.Dtos.Role;
using Pelo.Common.Dtos.User;
using Pelo.Web.Attributes;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Services.MasterServices;
using Pelo.Web.Services.UserServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class UserController : BaseController
    {
        private readonly IBranchService _branchService;

        private readonly IDepartmentService _departmentService;

        private readonly IRoleService _roleService;

        private readonly IUserService _userService;

        public UserController(IUserService userService,
                              IRoleService roleService,
                              IBranchService branchService,
                              IDepartmentService departmentService,
                              ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
            _roleService = roleService;
            _branchService = branchService;
            _departmentService = departmentService;
        }

        private async Task<Tuple<IEnumerable<RoleSimpleModel>, string>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAll();
                if(roles.IsSuccess)
                    return new Tuple<IEnumerable<RoleSimpleModel>, string>(roles.Data,
                                                                           string.Empty);

                Logger.LogInformation(roles.Message);
                return new Tuple<IEnumerable<RoleSimpleModel>, string>(new List<RoleSimpleModel>(),
                                                                       roles.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<RoleSimpleModel>, string>(new List<RoleSimpleModel>(),
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

        private async Task<Tuple<IEnumerable<DepartmentSimpleModel>, string>> GetAllDepartments()
        {
            try
            {
                var departmentes = await _departmentService.GetAll();
                if(departmentes.IsSuccess)
                    return new Tuple<IEnumerable<DepartmentSimpleModel>, string>(departmentes.Data,
                                                                                 string.Empty);

                Logger.LogInformation(departmentes.Message);
                return new Tuple<IEnumerable<DepartmentSimpleModel>, string>(new List<DepartmentSimpleModel>(),
                                                                             departmentes.Message);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.ToString());
                return new Tuple<IEnumerable<DepartmentSimpleModel>, string>(new List<DepartmentSimpleModel>(),
                                                                             exception.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DatatableRequest request)
        {
            var result = await _userService.GetByPaging(request);
            if(result.IsSuccess) return Json(result.Data);

            return Json(DatatableResponse<GetUserPagingResponse>.Init(request.Draw));
        }

        public async Task<IActionResult> Index()
        {
            var roles = await GetAllRoles();
            ViewBag.Roles = roles.Item1.ToList();
            if(!string.IsNullOrEmpty(roles.Item2))
                ModelState.AddModelError("",
                                         roles.Item2);

            var branches = await GetAllBranches();
            ViewBag.Branches = branches.Item1.ToList();
            if(!string.IsNullOrEmpty(branches.Item2))
                ModelState.AddModelError("",
                                         branches.Item2);

            var departments = await GetAllDepartments();
            ViewBag.Departments = departments.Item1.ToList();
            if(!string.IsNullOrEmpty(departments.Item2))
                ModelState.AddModelError("",
                                         departments.Item2);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return Json(result);
        }
    }
}
