using System;
using System.Web.Security;
using BLL.Interface.Services;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IAccountService AccountService
            => (IAccountService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAccountService));

        public override bool IsUserInRole(string username, string roleName)
        {
            string roles = AccountService.GetUserByLogin(username).Roles;

            return string.Compare(roles, roleName, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public override string[] GetRolesForUser(string username)
        {
            string roles = AccountService.GetUserByLogin(username).Roles;
            var arrRoles = new string[1];

            arrRoles[0] = roles;

            return arrRoles;
        }



        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}