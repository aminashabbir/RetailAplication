using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailCustomer.DAL
{
    public class RoleReturnType
    {

        string displayText;
        public string DisplayText
        {
            get { return displayText; }
            set { displayText = value; }
        }

        string actionName;
        public string ActionName
        {
            get { return actionName; }
            set { actionName = value; }
        }

        string controlerName;
        public string ControlerName
        {
            get { return controlerName; }
            set { controlerName = value; }
        }

    }

    public class Roles
    {
        public Roles()
        {
            RoleCount = 0;
            role = "";
            RoleList = new List<RoleReturnType>();
        }

        int roleCount;

        public int RoleCount
        {
            get { return roleCount; }
            set { roleCount = value; }
        }
        string role;
        
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        List<RoleReturnType> roleList;

        public List<RoleReturnType> RoleList
        {
            get { return roleList; }
            set { roleList = value; }
        }
    
    }
}