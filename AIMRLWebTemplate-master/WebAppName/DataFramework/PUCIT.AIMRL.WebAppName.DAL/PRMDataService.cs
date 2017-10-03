using System;
using PUCIT.AIMRL.WebAppName.UI.Common;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PUCIT.AIMRL.Common;
using PUCIT.AIMRL.WebAppName.DAL;
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using PUCIT.AIMRL.Common.Logger;
using PUCIT.AIMRL.WebAppName.Entities.Enum;
using System.Data.Common;
using PUCIT.AIMRL.WebAppName.Entities.Security;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using System.Threading.Tasks;

namespace PUCIT.AIMRL.WebAppName.DAL
{
    public class PRMDataService
    {
        public static TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

        #region Stored Procedures

        private const String SP_DBO_SAVESTUDENT = "dbo.SaveStudent";
        private const String SP_DBO_DEACTIVATESTUDENT = "[dbo].[DeactivateStudent]";
        private const String SP_DBO_SEARCHSTUDENTS = "dbo.SearchStudents";

        private const String SP_DBO_GETAPPROVERHERIRACHYS = "dbo.GetApproverHerirachy";


        #endregion

        public PRMDataService()
        {
            Database.SetInitializer<PRMDataContext>(null);
        }

        #region Roles
        public int SaveRole(Roles role, DateTime pActivityTime, int pActivityBy)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.SaveRoles @0, @1, @2,@3,@4";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = role.Id },
                    new SqlParameter { ParameterName = "@1", Value = role.Name},
                    new SqlParameter { ParameterName = "@2", Value = role.Description},
                    new SqlParameter { ParameterName = "@3", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@4", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();
                return data;
            }
        }
        public bool EnableDisableRole(int pRoleID, Boolean pIsActiv, DateTime pActivityTime, int pActivityBy)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.EnableDisableRole @0, @1, @2, @3";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pRoleID},
                     new SqlParameter { ParameterName = "@1", Value = pIsActiv},
                    new SqlParameter { ParameterName = "@2", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@3", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return true;
            }
        }
        public List<Roles> GetAllRoles()
        {
            using (var db = new PRMDataContext())
            {
                return db.Roles.ToList();
            }
        }

        public List<int> GetRolesByUserID(int pUserID)
        {
            using (var db = new PRMDataContext())
            {
                var result = db.UserRoles.Where(p => p.UserId == pUserID).Select(p => p.RoleId).ToList();
                return result;
            }
        }

        public int SaveUserRoleMapping(int pUserID, List<int> pRoles)
        {
            using (var db = new PRMDataContext())
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("ID");

                foreach (var p in pRoles)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(p);
                }

                string query = "execute dbo.SaveUserRoleMapping @0, @1";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pUserID},
                     new SqlParameter { ParameterName = "@1", Value = dt, SqlDbType = SqlDbType.Structured, TypeName = "dbo.ArrayInt" },
                };

                var data = db.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return data;
            }
        }

        #endregion

        #region Permission
        public int SavePermission(PermissionsWithRoleID per, DateTime pActivityTime, int pActivityBy)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.SavePermission @0, @1,@2,@3,@4";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = per.Id},
                    new SqlParameter { ParameterName = "@1", Value = per.Name},
                    new SqlParameter { ParameterName = "@2", Value = per.Description},
                    new SqlParameter { ParameterName = "@3", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@4", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();
                return data;
            }
        }
        public bool EnableDisablePermission(int pPermissionID, Boolean pIsActiv, DateTime pActivityTime, int pActivityBy)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.EnableDisablePermission @0, @1, @2, @3";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pPermissionID},
                     new SqlParameter { ParameterName = "@1", Value = pIsActiv},
                    new SqlParameter { ParameterName = "@2", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@3", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return true;
            }

        }
        public List<Permissions> GetAllPermissions()
        {
            using (var db = new PRMDataContext())
            {
                string query = "execute dbo.GetAllPermissions ";
                var list = db.Database.SqlQuery<Permissions>(query).ToList();
                return list;
            }
        }

        public List<int> GetPermissionsByRoleID(int pRoleID)
        {
            using (var db = new PRMDataContext())
            {
                var result = db.PermissionsMapping.Where(p => p.RoleId == pRoleID).Select(p => p.PermissionId).ToList();
                return result;
            }
        }

        public int SaveRolePermissionMapping(int pRoleID, List<int> pPermissionsList)
        {
            using (var db = new PRMDataContext())
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("ID");

                foreach (var p in pPermissionsList)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(p);
                }

                string query = "execute dbo.SaveRolePermissionMapping @0, @1";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pRoleID},
                     new SqlParameter { ParameterName = "@1", Value = dt, SqlDbType = SqlDbType.Structured, TypeName = "dbo.ArrayInt" },
                };

                var data = db.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return data;
            }
        }

        #endregion

        #region Users
        public int SaveUsers(User u, DateTime pActivityTime, int pActivityBy)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.SaveUsers @0, @1, @2, @3,@4, @5,@6";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = u.UserId },
                    new SqlParameter { ParameterName = "@1", Value = u.Login},
                    new SqlParameter { ParameterName = "@2", Value = "123" },
                    new SqlParameter { ParameterName = "@3", Value = u.Name },
                    new SqlParameter { ParameterName = "@4", Value = u.Email },
                    new SqlParameter { ParameterName = "@5", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@6", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();
                return data;
            }
        }
        public bool EnableDisableUser(int pUserID, Boolean pIsActiv, DateTime pActivityTime, int pActivityBy)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.EnableDisableUser @0, @1, @2, @3";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pUserID},
                     new SqlParameter { ParameterName = "@1", Value = pIsActiv},
                    new SqlParameter { ParameterName = "@2", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@3", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return true;
            }
        }
        public List<User> GetAllUsers()
        {
            using (var db = new PRMDataContext())
            {
                return db.Users.ToList();
            }
        }
        public User GetUserByEmail(string emailAddress)
        {
            using (var db = new PRMDataContext())
            {
                var result = (from data in db.Users
                              where data.Email == emailAddress && data.IsActive == true
                              select data).FirstOrDefault();
                return result;
            }
        }
        public List<User> SearchUsers(User entity)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.SearchUsers @0, @1";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = entity.Name },
                    new SqlParameter { ParameterName = "@1", Value = entity.Email}
                 };

                var list = ctx.Database.SqlQuery<User>(query, args).ToList();
                return list;
            }
        }
        public List<Approver> SearchUser(string key)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.SearchUserForAutoComplete @0";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = key }
                };

                var list = ctx.Database.SqlQuery<Approver>(query, args).ToList();
                return list;
            }
        }
        public List<LoginHistory> GetLoginHistory()
        {
            using (var db = new PRMDataContext())
            {
                string query = "select * from dbo.LoginHistory Order by LoginTime Desc";
                List<LoginHistory> log = db.Database.SqlQuery<LoginHistory>(query).ToList();

                foreach (var l in log)
                {
                    l.LoginTime = l.LoginTime.ToTimeZoneTime(tzi);
                }

                return log;
            }
        }
        public int changePassword(PasswordEntity pass)
        {
            var username = SessionManager.GetUserLogin();

            using (var db = new PRMDataContext())
            {
                var query = db.Users.Where(x => (x.Login == username) && (x.Password == pass.CurrentPassword)).FirstOrDefault();

                if (query != null)
                {
                    query.Password = pass.NewPassword;

                    db.SaveChanges();
                    return 1;

                }
                else return 0;

            }
        }
        public int resetPassword(String emailAddress, String password)
        {
            using (var db = new PRMDataContext())
            {

                var query = (from data in db.Users
                             where data.Email == emailAddress
                             select data).SingleOrDefault();

                query.Password = password;

                db.SaveChanges();
                return 1;
            }
        }
        public SecUserDTO ValidateUserSP(String pLogin, String pPassword, DateTime pCurrTime, String pMachineIP, Boolean pIgnorePassword, String pLoggerLoginID)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.ValidateUser @0, @1, @2, @3,@4,@5";

                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@0", Value = pLogin });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = pPassword });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = pCurrTime.YYYYMMDD() });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = pMachineIP });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = pIgnorePassword });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = pLoggerLoginID });


                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();


                // Read User from the first result set
                var user = ((IObjectContextAdapter)ctx)
                    .ObjectContext
                    .Translate<User>(reader).FirstOrDefault();

                if (user != null)
                {
                    var secUserForSession = new SecUserDTO();
                    if (user.IsActive == false)
                    {
                        secUserForSession.IsActive = user.IsActive;
                    }
                    else
                    {
                        reader.NextResult();
                        var roles = ((IObjectContextAdapter)ctx)
                            .ObjectContext
                            .Translate<Roles>(reader).ToList();

                        reader.NextResult();
                        var permissions = ((IObjectContextAdapter)ctx)
                            .ObjectContext
                            .Translate<PermissionsWithRoleID>(reader).ToList();

                        reader.Close();

                        secUserForSession.Login = user.Login;
                        secUserForSession.UserFullName = user.Name;
                        secUserForSession.UserId = user.UserId;
                        secUserForSession.Email = user.Email;
                        secUserForSession.IsActive = user.IsActive;
                        secUserForSession.Permissions = new List<string>();
                        secUserForSession.Roles = new List<string>();
                        

                        foreach (var r in roles)
                        {
                            secUserForSession.Roles.Add(r.Name);
                        }

                        foreach (var p in permissions)
                        {
                            secUserForSession.Permissions.Add(p.Name.ToUpper());
                        }
                    }
                    return secUserForSession;
                }

                reader.Close();
                return null;
            }
        }

        public List<String> GetRolePermissionById(int pUserID, out List<String> pRoles)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute dbo.GetRolePermissionById @0";
                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@0", Value = pUserID });

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();


                var roles = ((IObjectContextAdapter)ctx)
                        .ObjectContext
                        .Translate<Roles>(reader).ToList();

                reader.NextResult();
                var permissions = ((IObjectContextAdapter)ctx)
                    .ObjectContext
                    .Translate<PermissionsWithRoleID>(reader).ToList();

                reader.Close();

                var rolesList = new List<String>();
                var permList = new List<String>();

                foreach (var r in roles)
                {
                    rolesList.Add(r.Name);
                }

                foreach (var p in permissions)
                {
                    permList.Add(p.Name.ToUpper());
                }

                pRoles = rolesList;

                return permList;
            }
        }
        #endregion

        #region RolePermission Mapping
        public bool UpdateMappings(customUpdateMappings m)
        {
            try
            {
                using (var db = new PRMDataContext())
                {
                    string query = "select * from dbo.PermissionsMapping where RoleId=" + m.Roleid;
                    List<PermissionsMapping> permission = db.Database.SqlQuery<PermissionsMapping>(query).ToList();
                    //for(int i=0;i < permission.Count;i++)
                    //{
                    //    var entry = db.Entry(permission[i]);
                    //    if (entry.State == EntityState.Detached)
                    //        db.PermissionsMapping.Attach(permission[i]);
                    //    db.PermissionsMapping.Remove(permission[i]);
                    //}
                    //db.SaveChanges();
                    foreach (var e in permission)
                    {
                        int index = m.Permissions.FindIndex(x => x.Id == e.PermissionId);
                        if (m.Permissions[index].Exist == false)
                        {
                            var entry = db.Entry(e);
                            if (entry.State == EntityState.Detached)
                            {
                                db.PermissionsMapping.Attach(e);
                            }
                            db.PermissionsMapping.Remove(e);
                            db.SaveChanges();
                        }
                        else
                        {
                            m.Permissions[index].Exist = false;
                        }
                    }
                    foreach (var e in m.Permissions)
                    {
                        if (e.Exist == true)
                        {
                            PermissionsMapping obj = new PermissionsMapping();
                            obj.RoleId = m.Roleid;
                            obj.PermissionId = e.Id;

                            db.PermissionsMapping.Add(obj);
                        }
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteLog("loggedinuserloginid", ex.Message, PUCIT.AIMRL.Common.Logger.LogType.ErrorMsg, ex);
                throw;
            }
            return true;
        }
        public bool DeleteMappings(int rid)
        {
            try
            {
                using (var db = new PRMDataContext())
                {
                    string query = "execute dbo.DeletePermissiosMapping @0";
                    var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = rid}
                };

                    var data = db.Database.SqlQuery<int>(query, args).FirstOrDefault();
                    if (data > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteLog("loggedinuserloginid", ex.Message, PUCIT.AIMRL.Common.Logger.LogType.ErrorMsg, ex);
                throw;
            }
            return true;
        }
        public List<customMappings> getMappings()
        {
            List<customMappings> list = new List<customMappings>();
            List<PermissionsMapping> lpm = new List<PermissionsMapping>();
            using (var db = new PRMDataContext())
            {
                lpm = db.PermissionsMapping.ToList();
                List<int> rid = new List<int>();

                foreach (var p in lpm)
                {
                    if (rid.IndexOf(p.RoleId) < 0)
                    {
                        rid.Add(p.RoleId);

                        customMappings cm = new customMappings();
                        cm.mappings = db.PermissionsMapping.Where(x => x.RoleId == p.RoleId).ToList();
                        cm.roles = db.Roles.Where(x => x.Id == p.RoleId).FirstOrDefault();
                        foreach (PermissionsMapping i in cm.mappings)
                        {
                            string query = "Select * from  dbo.Permissions where Id=" + i.PermissionId;
                            Permissions permission = db.Database.SqlQuery<Permissions>(query).FirstOrDefault();
                            cm.permissions.Add(permission);
                        }

                        list.Add(cm);

                    }
                }
            }
            return list;
        }

        #endregion

        #region EmailRequests

        public List<EmailRequest> GetEmailRequestsForProcessing()
        {

            try
            {
                using (var ctx = new PRMDataContext())
                {
                    var list = ctx.EmailRequests.Where(p => p.EmailRequestStatus == (int)EmailRequestStatus.Pending).OrderBy(p => p.EmailRequestID).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                return new List<EmailRequest>();
            }
        }
        public void ProcessEmailRequests(List<long> list)
        {
            using (var ctx = new PRMDataContext())
            {
                foreach (var id in list)
                {
                    var dto = new EmailRequest() { EmailRequestID = id, EmailRequestStatus = (int)EmailRequestStatus.Processed };
                    ctx.EmailRequests.Attach(dto);
                    var entry = ctx.Entry(dto);
                    entry.State = EntityState.Unchanged;
                    entry.Property(e => e.EmailRequestStatus).IsModified = true;
                }

                ctx.SaveChanges();

            }
        }
        public List<EmailRequest> GetEmailRequestsByUniqueID(PRMDataContext ctx, String pUniqueID)
        {
            try
            {
                var data = new List<EmailRequest>();
                string query = "execute dbo.GetEmailRequestsByUniqueID @0";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = pUniqueID}
                    };

                if (ctx != null)
                {
                    data = ctx.Database.SqlQuery<EmailRequest>(query, args).ToList();
                }
                else
                {
                    using (ctx = new PRMDataContext())
                    {
                        data = ctx.Database.SqlQuery<EmailRequest>(query, args).ToList();

                    }
                }
                return data;
            }
            catch (Exception)
            {
                return new List<EmailRequest>();
            }
        }
        private void ProcessEmails(List<EmailRequest> pEmails)
        {
            System.Threading.Thread th = new System.Threading.Thread(delegate(Object a)
            {

                try
                {
                    var pEmailRequests = (List<EmailRequest>)a;
                    var list = new List<long>();
                    foreach (var email in pEmailRequests)
                    {
                        EmailHandler.SendEmail(email.EmailTo, email.Subject, email.MessageBody);
                        list.Add(email.EmailRequestID);
                    }
                    ProcessEmailRequests(list);
                }
                catch (Exception)
                {
                }
            });

            th.Start(pEmails);

        }

        #endregion


        




    }
}
