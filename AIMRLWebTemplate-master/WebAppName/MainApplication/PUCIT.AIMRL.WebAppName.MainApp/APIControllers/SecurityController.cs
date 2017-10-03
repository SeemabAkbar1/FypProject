using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using PUCIT.AIMRL.WebAppName.MainApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace PUCIT.AIMRL.WebAppName.MainApp.APIControllers
{
    public class SecurityController : BaseDataController
    {
        //
        // GET: /Admin/

        private readonly SecurityRepository _repository;

        public SecurityController()
        {
            _repository = new SecurityRepository();
        }
        private SecurityRepository Repository
        {
            get
            {
                return _repository;
            }
        }
        [HttpGet]
        public Object getRoles()
        {
            return Repository.getRoles();
        }
        [HttpGet]
        public Object getActiveRoles()
        {
            return Repository.getActiveRoles();
        }

        [HttpPost]
        public Object SaveRole(Roles r)
        {
            return Repository.SaveRole(r);
        }
        [HttpPost]
        public Object EnableDisableRole(Roles r)
        {
            return Repository.EnableDisableRole(r);
        }
        //[HttpGet]
        //public Object getMappings()
        //{
        //    return Repository.getMappings();
        //}
        [HttpGet]
        public Object getPermissions()
        {
            return Repository.getPermissions();
        }

        [HttpGet]
        public Object getActivePermissions()
        {
            return Repository.getActivePermissions();
        }
        [HttpGet]
        public Object GetPermissionsByRoleID(int pRoleID)
        {
            return Repository.GetPermissionsByRoleID(pRoleID);
        }
        [HttpPost]
        public Object SaveRolePermissionMapping(TempRolePermMapping r)
        {
            return Repository.SaveRolePermissionMapping(r.RoleID, r.Permissions);
        }

        //[HttpPost]
        //public Object UpdateMappings(customUpdateMappings m)
        //{
        //    return Repository.UpdateMappings(m);
        //}
        //[HttpPost]
        //public Object DeleteMappings(int roleid)
        //{
        //    return Repository.DeleteMappings(roleid);
        //}
        [HttpPost]
        public Object SavePermission(PermissionsWithRoleID r)
        {
            return Repository.SavePermission(r);
        }
        [HttpPost]
        public Object EnableDisablePermission(PermissionsWithRoleID r)
        {
            return Repository.EnableDisablePermission(r);
        }


        [HttpGet]
        public Object getUsers()
        {
            return Repository.getUsers();
        }

        [HttpPost]
        public object SaveUsers(User u)
        {
            return Repository.SaveUsers(u);
        }

        [HttpPost]
        public object EnableDisableUser(User u)
        {
            return Repository.EnableDisableUser(u);
        }

        [HttpGet]
        public Object GetRolesByUserID(int pUserID)
        {
            return Repository.GetRolesByUserID(pUserID);
        }
        [HttpPost]
        public Object SaveUserRoleMapping(TempUserRoleMapping r)
        {
            return Repository.SaveUserRoleMapping(r.UserID, r.Roles);
        }
    }
    public class TempRolePermMapping
    {
        public int RoleID { get; set; }
        public List<int> Permissions { get; set; }
    }

    public class TempUserRoleMapping
    {
        public int UserID { get; set; }
        public List<int> Roles { get; set; }
    }
}
