using IIRS.IRepository;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities;
using IIRS.Utilities.Common;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class PermissionController : ControllerBase
    {
        readonly IPermissionRepository _permissionRepository;
        readonly IModuleRepository _moduleRepository;
        readonly IRoleModulePermissionRepository _roleModulePermissionRepository;
        readonly IUserRoleRepository _userRoleRepository;
        readonly IHttpContextAccessor _httpContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionRepository"></param>
        /// <param name="moduleRepository"></param>
        /// <param name="roleModulePermissionRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="httpContext"></param>
        public PermissionController(IPermissionRepository permissionRepository, IModuleRepository moduleRepository,
            IRoleModulePermissionRepository roleModulePermissionRepository, IUserRoleRepository userRoleRepository, IHttpContextAccessor httpContext)
        {
            _permissionRepository = permissionRepository;
            _moduleRepository = moduleRepository;
            _roleModulePermissionRepository = roleModulePermissionRepository;
            _userRoleRepository = userRoleRepository;
            _httpContext = httpContext;

        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageModel<Sys_Permission>>> Get(int page = 1, string key = "")
        {
            PageModel<Sys_Permission> permissions = new PageModel<Sys_Permission>();
            int intPageSize = 50;
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }

            permissions = await _permissionRepository.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");


            #region 单独处理

            var apis = await _moduleRepository.Query(d => d.IsDeleted == false);
            var permissionsView = permissions.data;

            var permissionAll = await _permissionRepository.Query(d => d.IsDeleted != true);
            foreach (var item in permissionsView)
            {
                List<Guid> pidarr = new List<Guid> { };

                var parent = permissionAll.FirstOrDefault(d => d.ID == item.Pid);

                while (parent != null)
                {
                    pidarr.Add(parent.ID);
                    parent = permissionAll.FirstOrDefault(d => d.ID == parent.Pid);
                }

                pidarr.Reverse();
                pidarr.Insert(0, Guid.Empty);
                item.PidArr = pidarr;

                foreach (var pid in item.PidArr)
                {
                    var per = permissionAll.FirstOrDefault(d => d.ID == pid);
                    item.PnameArr.Add((per != null ? per.Name : "根节点") + "/");
                }

                item.MName = apis.FirstOrDefault(d => d.ID == item.Mid)?.LinkUrl;
            }

            permissions.data = permissionsView;

            #endregion


            return new MessageModel<PageModel<Sys_Permission>>()
            {
                msg = "获取成功",
                success = permissions.dataCount >= 0,
                response = permissions
            };

        }

        /// <summary>
        /// 查询树形 Table
        /// </summary>
        /// <param name="f">父节点</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<List<Sys_Permission>>> GetTreeTable(Guid f = new Guid(), string key = "")
        {
            List<Sys_Permission> permissions = new List<Sys_Permission>();
            var apiList = await _moduleRepository.Query(d => d.IsDeleted == false);
            var permissionsList = await _permissionRepository.Query(d => d.IsDeleted == false);
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }

            if (key != "")
            {
                permissions = permissionsList.Where(a => a.Name.Contains(key)).OrderBy(a => a.OrderSort).ToList();
            }
            else
            {
                permissions = permissionsList.Where(a => a.Pid == f).OrderBy(a => a.OrderSort).ToList();
            }

            foreach (var item in permissions)
            {
                List<Guid> pidarr = new List<Guid>
                {
                    item.Pid
                };
                if (item.Pid != Guid.Empty)
                {
                    pidarr.Add(Guid.Empty);
                }
                var parent = permissionsList.FirstOrDefault(d => d.ID == item.Pid);

                while (parent != null)
                {
                    pidarr.Add(parent.ID);
                    parent = permissionsList.FirstOrDefault(d => d.ID == parent.Pid);
                }


                item.PidArr = pidarr.OrderBy(d => d).Distinct().ToList();
                item.MName = apiList.FirstOrDefault(d => d.ID == item.Mid)?.LinkUrl;
                item.HasChildren = permissionsList.Where(d => d.Pid == item.ID).Any();
            }

            return new MessageModel<List<Sys_Permission>>()
            {
                msg = "获取成功",
                success = permissions.Count >= 0,
                response = permissions
            };
        }

        /// <summary>
        /// 添加一个菜单
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Sys_Permission permission)
        {
            var permissionList = await _permissionRepository.Query(d => d.Name == permission.Name && d.Pid == permission.Pid && d.IsDeleted == false);
            if (permissionList.Count > 0)
            {
                return new MessageModel<string>()
                {
                    msg = $"当前级别下，菜单 {permission.Name} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<string>();

            ////创建编号
            permission.ID = Provider.Sql.Create();

            var id = (await _permissionRepository.Add(permission));
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 保存菜单权限分配
        /// </summary>
        /// <param name="assignView"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Assign([FromBody] AssignView assignView)
        {
            var data = new MessageModel<string>();

            try
            {
                if (assignView.Rid != Guid.Empty)
                {
                    data.success = true;

                    var roleModulePermissions = await _roleModulePermissionRepository.Query(d => d.RoleId == assignView.Rid);

                    var remove = roleModulePermissions.Where(d => !assignView.Pids.Contains(Guid.Parse(d.PermissionId.ToString()))).Select(c => (object)c.ID);
                    data.success |= await _roleModulePermissionRepository.DeleteByIds(remove.ToArray());

                    foreach (var item in assignView.Pids)
                    {
                        var rmpitem = roleModulePermissions.Where(d => d.PermissionId == item);
                        if (!rmpitem.Any())
                        {
                            var moduleid = (await _permissionRepository.Query(p => p.ID == item)).FirstOrDefault()?.Mid;
                            Sys_RoleModulePermission roleModulePermission = new Sys_RoleModulePermission()
                            {
                                IsDeleted = false,
                                RoleId = assignView.Rid,
                                ModuleId = moduleid == Guid.Empty?null:moduleid,
                                PermissionId = item,
                            };

                            data.success |= (await _roleModulePermissionRepository.Add(roleModulePermission)) > 0;

                        }
                    }

                    if (data.success)
                    {
                        data.response = "";
                        data.msg = "保存成功";
                    }

                }
            }
            catch (Exception)
            {
                data.success = false;
            }

            return data;
        }


        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="needbtn"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PermissionTree>> GetPermissionTree(Guid pid = new Guid(), bool needbtn = false)
        {
            var data = new MessageModel<PermissionTree>();

            var permissions = await _permissionRepository.Query(d => d.IsDeleted == false);
            var permissionTrees = (from child in permissions
                                   where child.IsDeleted == false
                                   orderby child.ID
                                   select new PermissionTree
                                   {
                                       Value = child.ID,
                                       Label = child.Name,
                                       Pid = child.Pid,
                                       Isbtn = child.IsButton,
                                       Order = child.OrderSort,
                                   }).ToList();
            PermissionTree rootRoot = new PermissionTree
            {
                Value = Guid.Empty,
                Pid = Guid.Empty,
                Label = "根节点"
            };

            permissionTrees = permissionTrees.OrderBy(d => d.Order).ToList();


            Recursion.LoopToAppendChildren(permissionTrees, rootRoot, pid, needbtn);

            data.success = true;
            if (data.success)
            {
                data.response = rootRoot;
                data.msg = "获取成功";
            }

            return data;
        }

        /// <summary>
        /// 获取路由树
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<NavigationBar>> GetNavigationBar(Guid uid)
        {

            var data = new MessageModel<NavigationBar>();

            // 两种方式获取 uid
            var uidInHttpcontext = Guid.Parse((from item in _httpContext.HttpContext.User.Claims
                                               where item.Type == "jti"
                                               select item.Value).FirstOrDefault());

            //var uidInHttpcontext = (JwtHelper.SerializeJwt(_httpContext.HttpContext.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "")))?.Uid;

            if (uid != Guid.Empty && uid == uidInHttpcontext)
            {
                var roleIds = (await _userRoleRepository.Query(d => d.IsDeleted == false && d.UserId == uid)).Select(d => d.RoleId).Distinct();
                if (roleIds.Any())
                {
                    var pids = (await _roleModulePermissionRepository.Query(d => d.IsDeleted == false && roleIds.Contains(d.RoleId))).Select(d => d.PermissionId).Distinct();
                    if (pids.Any())
                    {
                        var rolePermissionMoudles = (await _permissionRepository.Query(d => pids.Contains(d.ID))).OrderBy(c => c.OrderSort);
                        var permissionTrees = (from child in rolePermissionMoudles
                                               where child.IsDeleted == false
                                               orderby child.ID
                                               select new NavigationBar
                                               {
                                                   Id = child.ID,
                                                   Name = child.Name,
                                                   Description = child.Description,
                                                   Redirect = child.Redirect,
                                                   Pid = child.Pid,
                                                   Order = child.OrderSort,
                                                   Path = child.Code,
                                                   IconCls = child.Icon,
                                                   Func = child.Func,
                                                   IsHide = child.IsHide.ObjToBool(),
                                                   IsButton = child.IsButton.ObjToBool(),
                                                   Meta = new NavigationBarMeta
                                                   {
                                                       RequireAuth = true,
                                                       Title = child.Name,
                                                       NoTabPage = child.IsHide.ObjToBool()
                                                   }
                                               }).ToList();


                        NavigationBar rootRoot = new NavigationBar()
                        {
                            Id = Guid.Empty,
                            Pid = Guid.Empty,
                            Order = 0,
                            Name = "根节点",
                            Redirect = "",
                            Path = "",
                            IconCls = "",
                            Meta = new NavigationBarMeta(),

                        };

                        permissionTrees = permissionTrees.OrderBy(d => d.Order).ToList();

                        Recursion.LoopNaviBarAppendChildren(permissionTrees, rootRoot);

                        data.success = true;
                        if (data.success)
                        {
                            data.response = rootRoot;
                            data.msg = "获取成功";
                        }
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 通过角色获取菜单【无权限】
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<AssignShow>> GetPermissionIdByRoleId(Guid rid = new Guid())
        {
            var data = new MessageModel<AssignShow>();

            var rmps = await _roleModulePermissionRepository.Query(d => d.IsDeleted == false && d.RoleId == rid);
            var permissionTrees = (from child in rmps
                                   orderby child.ID
                                   select child.PermissionId).ToList();

            var permissions = await _permissionRepository.Query(d => d.IsDeleted == false);
            List<string> assignbtns = new List<string>();

            foreach (var item in permissionTrees)
            {
                var pername = permissions.FirstOrDefault(d => d.IsButton && d.ID == item)?.Name;
                if (!string.IsNullOrEmpty(pername))
                {
                    assignbtns.Add(pername + "_" + item);
                }
            }

            data.success = true;
            if (data.success)
            {
                data.response = new AssignShow()
                {
                    Permissionids = permissionTrees,
                    Assignbtns = assignbtns,
                };
                data.msg = "获取成功";
            }

            return data;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Sys_Permission permission)
        {
            var data = new MessageModel<string>();
            if (permission != null && permission.ID != Guid.Empty)
            {
                data.success = await _permissionRepository.Update(permission);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = permission?.ID.ToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(Guid id)
        {
            var data = new MessageModel<string>();
            if (id != Guid.Empty)
            {
                var userDetail = await _permissionRepository.QueryById(id);
                userDetail.IsDeleted = true;
                data.success = await _permissionRepository.Update(userDetail);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = userDetail?.ID.ToString();
                }
            }

            return data;
        }
    }

    public class AssignView
    {
        public List<Guid> Pids { get; set; }
        public Guid Rid { get; set; }
    }
    public class AssignShow
    {
        public List<Guid?> Permissionids { get; set; }
        public List<string> Assignbtns { get; set; }
    }
}
