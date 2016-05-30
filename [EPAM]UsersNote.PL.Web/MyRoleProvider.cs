using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using _EPAM_UsersNote.BLL_Logic;
using _EPAM_UsersNote.Interfaces.BLL;

namespace _EPAM_UsersNote.PL.Web
{
    public class MyRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            IUsersNoteAuthuserBLL authuser = new UsersNoteAuthuserLogic();
            return (authuser.GetUserRole(username) == roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            IUsersNoteAuthuserBLL authuser = new UsersNoteAuthuserLogic();
            string role = authuser.GetUserRole(username);
            return role.Split(':');
        }

        #region NotImplemented
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }



        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }



        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}