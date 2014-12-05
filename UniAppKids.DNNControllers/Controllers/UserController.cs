using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniAppKids.DNNControllers.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security.Membership;
    using DotNetNuke.Security.Roles;

    using UniAppKids.DNNControllers.Services;

    public class UserController : ControllerBase
    {
        public void createDnnUser(string UserName)
        {
            UserInfo newUser = new UserInfo();
            newUser.Username = UserName;
            newUser.PortalID = PortalSettings.PortalId;
            newUser.DisplayName = "John Doe";
            newUser.Email = "jdoe@email.com";
            newUser.FirstName = "John";
            newUser.LastName = "Doe";
            newUser.Profile.SetProfileProperty("SSN", "123-456-7890");

            UserCreateStatus rc = DotNetNuke.Entities.Users.UserController.CreateUser(ref newUser);
            if (rc == UserCreateStatus.Success)
            {
                // Manual add role to user
                addRoleToUser(newUser, "Registered Users", DateTime.MaxValue);
            }
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage checkUserAuthenticated()
        {
            var authenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            if (authenticated)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo().Username);
            }

            return ControllerContext.Request.CreateResponse(
          HttpStatusCode.Unauthorized, "Not authorized");

        }

        public bool addRoleToUser(UserInfo user, string roleName, DateTime expiry)
        {
            var rc = false;
            var roleCtl = new RoleController();
            RoleInfo newRole = roleCtl.GetRoleByName(user.PortalID, roleName);
            if (newRole != null && user != null)
            {
                rc = user.IsInRole(roleName);
                roleCtl.AddUserRole(user.PortalID, user.UserID, newRole.RoleID, DateTime.MinValue, expiry);
                // Refresh user and check if role was added
                user = DotNetNuke.Entities.Users.UserController.GetUserById(user.PortalID, user.UserID);
                rc = user.IsInRole(roleName);
            }
            return rc;
        }
    }
}