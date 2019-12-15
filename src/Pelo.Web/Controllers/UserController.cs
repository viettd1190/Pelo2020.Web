using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Branch;
using Pelo.Common.Dtos.Department;
using Pelo.Common.Dtos.Role;
using Pelo.Common.Dtos.User;
using Pelo.Common.Extensions;
using Pelo.Common.Models;
using Pelo.Web.Attributes;
using Pelo.Web.Models.Datatables;
using Pelo.Web.Models.User;
using Pelo.Web.Services.MasterServices;
using Pelo.Web.Services.UserServices;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class UserController : BaseController
    {
        private readonly IBranchService _branchService;

        private readonly IDepartmentService _departmentService;

        readonly IMapper _mapper;

        private readonly IRoleService _roleService;

        private readonly IUserService _userService;

        public UserController(IUserService userService,
                              IRoleService roleService,
                              IBranchService branchService,
                              IDepartmentService departmentService,
                              IMapper mapper,
                              ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
            _roleService = roleService;
            _branchService = branchService;
            _departmentService = departmentService;
            _mapper = mapper;
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

        private async Task SetViewBag()
        {
            var roles = await GetAllRoles();
            ViewBag.Roles = roles.Item1.ToList();
            if (!string.IsNullOrEmpty(roles.Item2))
                ModelState.AddModelError("",
                                         roles.Item2);

            var branches = await GetAllBranches();
            ViewBag.Branches = branches.Item1.ToList();
            if (!string.IsNullOrEmpty(branches.Item2))
                ModelState.AddModelError("",
                                         branches.Item2);

            var departments = await GetAllDepartments();
            ViewBag.Departments = departments.Item1.ToList();
            if (!string.IsNullOrEmpty(departments.Item2))
                ModelState.AddModelError("",
                                         departments.Item2);
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
            await SetViewBag();

            return View();
        }

        public async Task<IActionResult> Add()
        {
            await SetViewBag();
            return View(new InsertUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(InsertUserModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.Insert(_mapper.Map<InsertUserModel, InsertUserRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "User");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            await SetViewBag();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await _userService.GetById(id);
                if(model.IsSuccess)
                {
                    await SetViewBag();
                    return View(_mapper.Map<GetUserByIdResponse, UserModel>(model.Data));
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
        public async Task<IActionResult> Edit(UserModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.Update(_mapper.Map<UserModel, UpdateUserRequest>(model));
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index",
                                            "User");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            await SetViewBag();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return Json(result);
        }
    }
}
