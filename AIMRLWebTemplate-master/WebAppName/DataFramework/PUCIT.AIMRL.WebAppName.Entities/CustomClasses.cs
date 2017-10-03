using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PUCIT.AIMRL.WebAppName.Entities
{
    
    
    public class customMappings
    {
        public List<PermissionsMapping> mappings { get; set; }
        public Roles roles { get; set; }
        public List<Permissions> permissions;
        public customMappings()
        {
            mappings = new List<PermissionsMapping>();
            permissions = new List<Permissions>();
            roles = new Roles();
        }
    }
    public class customPermissions
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Boolean Exist { get; set; }

    }
    public class customUpdateMappings
    {
        public int Roleid { get; set; }

        public List<customPermissions> Permissions;
        public customUpdateMappings()
        {
            Permissions = new List<customPermissions>();

        }
    }
}
