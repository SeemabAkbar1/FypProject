using PUCIT.AIMRL.WebAppName.DAL;
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using PUCIT.AIMRL.WebAppName.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
namespace PUCIT.AIMRL.WebAppName.MainApp.APIControllers
{
    class SecurityRepository
    {
        private PRMDataService _dataService;
        public SecurityRepository()
        {

        }
        private PRMDataService DataService
        {
            get
            {
                if (_dataService == null)
                    _dataService = new PRMDataService();

                return _dataService;
            }
        }

        #region Permissions

        public object getPermissions()
        {
            try
            {
                var List = DataService.GetAllPermissions();
                return (new
                {
                    data = new
                    {
                        PermissionList = List
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        public object getActivePermissions()
        {
            try
            {
                var List = DataService.GetAllPermissions().Where(p => p.IsActive == true).ToList();
                return (new
                {
                    data = new
                    {
                        PermissionList = List
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        public object EnableDisablePermission(PermissionsWithRoleID r)
        {
            String msg = " ";
            try
            {

                bool rowdeleted = DataService.EnableDisablePermission(r.Id, r.IsActive, DateTime.UtcNow, SessionManager.CurrentUser.UserId);

                if (rowdeleted == true)
                {
                    var param = (r.IsActive == false ? "disabled" : "enabled");
                    msg = String.Format("Permission is {0} successfully", param);
                }
                else
                {
                    msg = " ";
                }
                return (new
                {

                    success = true,
                    error = msg
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        public object SavePermission(PermissionsWithRoleID p)
        {
            String msg = " ";
            try
            {
                var permId = DataService.SavePermission(p, DateTime.UtcNow, SessionManager.CurrentUser.UserId);
                if (p.Id > 0)
                {
                    msg = "Permission Updated Successfully";
                }
                if (p.Id == 0)
                {
                    msg = "Permission Added Successfully";
                }

                return (new
                {
                    data = new
                    {
                        PermssionId = permId
                    },
                    success = true,
                    error = msg
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        public object GetPermissionsByRoleID(int pRoleID)
        {
            try
            {
                var List = DataService.GetPermissionsByRoleID(pRoleID);

                return (new
                {
                    data = new
                    {
                        Permissions = List
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }

        public object SaveRolePermissionMapping(int pRoleID, List<int> pPermissionsList)
        {
            try
            {
                var List = DataService.SaveRolePermissionMapping(pRoleID, pPermissionsList);

                return (new
                {
                    data = new
                    {
                        Permissions = List
                    },
                    success = true,
                    error = "Mappings are saved"
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }

        #endregion

        #region Roles
        public Object getRoles()
        {
            try
            {
                var List = DataService.GetAllRoles();
                return (new
                {
                    data = new
                    {
                        RoleList = List
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        public Object EnableDisableRole(Roles pRoleObj)
        {
            String msg = " ";
            try
            {
                bool rowdeleted = DataService.EnableDisableRole(pRoleObj.Id, pRoleObj.IsActive, DateTime.UtcNow, SessionManager.CurrentUser.UserId);

                if (rowdeleted == true)
                {
                    var param = (pRoleObj.IsActive == false ? "disabled" : "enabled");
                    msg = String.Format("Role is {0} successfully", param);
                }
                else
                {
                    msg = " ";
                }
                return (new
                {

                    success = true,
                    error = msg
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        public Object SaveRole(Roles r)
        {
            String msg = " ";
            try
            {
                var roleId = DataService.SaveRole(r, DateTime.UtcNow, SessionManager.CurrentUser.UserId);
                if (r.Id > 0)
                {
                    msg = "Role Updated Successfully";
                }
                if (r.Id == 0)
                {
                    msg = "Role added Successfully";
                }
                return (new
                {
                    data = new
                    {
                        RoleId = roleId
                    },
                    success = true,
                    error = msg
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        public object getActiveRoles()
        {
            try
            {
                var List = DataService.GetAllRoles().Where(p => p.IsActive == true).ToList();
                return (new
                {
                    data = new
                    {
                        RoleList = List
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }

        public object GetRolesByUserID(int pUserID)
        {
            try
            {
                var List = DataService.GetRolesByUserID(pUserID);

                return (new
                {
                    data = new
                    {
                        Roles = List
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }

        public object SaveUserRoleMapping(int pUserID, List<int> pRoles)
        {
            try
            {
                var List = DataService.SaveUserRoleMapping(pUserID, pRoles);

                return (new
                {
                    data = new
                    {
                        Roles = List
                    },
                    success = true,
                    error = "Mappings are saved"
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
        #endregion


        #region Users

        public object EnableDisableUser(User pUserObj)
        {
            try
            {
                String msg = "";
                var result = DataService.EnableDisableUser(pUserObj.UserId, pUserObj.IsActive, DateTime.UtcNow, SessionManager.CurrentUser.UserId);
                if (result == true)
                {
                    var param = (pUserObj.IsActive == false ? "disabled" : "enabled");
                    msg = String.Format("User is {0} successfully", param);
                }
                else
                {
                    msg = "";
                }


                if (result == true)
                {
                    return (new
                    {
                        data = new
                        {
                            UserList = result
                        },
                        success = true,
                        error = msg
                    });

                }
                else
                {

                    return (new
                    {
                        success = false,
                        error = "Some Error has occurred"
                    });


                }

            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }

        }

        public Object SaveUsers(User u)
        {
            try
            {
                String msg;
                var result = DataService.SaveUsers(u, DateTime.UtcNow, SessionManager.CurrentUser.UserId);
                if (result > 0)
                {
                    if (u.UserId > 0)
                    {
                        msg = "User Updated Successfully";
                    }
                    else
                    {
                        msg = "User Added Successfully";
                    }

                    return (new
                    {
                        data = new
                        {
                            UserId = result
                        },
                        success = true,
                        error = msg
                    });

                }
                else
                {
                    return (new
                    {
                        success = false,
                        error = "Some Error has occurred"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }

        public Object getUsers()
        {
            try
            {
                var List = DataService.GetAllUsers();
                return (new
                {
                    data = new
                    {
                        UserList = List
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }


        #endregion

        //public Object getMappings()
        //{
        //    try
        //    {
        //        var List = DataService.getMappings();
        //        return (new
        //        {
        //            data = new
        //            {
        //                MappingList = List
        //            },
        //            success = true,
        //            error = ""
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return (new
        //        {
        //            success = false,
        //            error = "Some Error has occurred"
        //        });
        //    }
        //}
        //public Object UpdateMappings(customUpdateMappings m)
        //{
        //    String msg = " ";
        //    try
        //    {
        //        var flag = DataService.UpdateMappings(m);
        //        msg = "mapping updated";
        //        return (new
        //        {
        //            success = true,
        //            error = msg
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return (new
        //        {
        //            success = false,
        //            error = "Some Error has occurred"
        //        });
        //    }
        //}
        //public Object DeleteMappings(int rid)
        //{
        //    String msg = " ";
        //    try
        //    {
        //        var flag = DataService.DeleteMappings(rid);
        //        msg = "mapping Deleted";
        //        return (new
        //        {
        //            success = true,
        //            error = msg
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return (new
        //        {
        //            success = false,
        //            error = "Some Error has occurred"
        //        });
        //    }
        //}

    }
}