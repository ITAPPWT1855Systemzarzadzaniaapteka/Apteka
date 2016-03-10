using Apteka.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apteka.Startup))]
namespace Apteka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        private void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            foreach (var role in AppConfig.defaultRoles) {
                if (!roleManager.RoleExists(role)) {
                    roleManager.Create(new IdentityRole { Name = role });
                }
            }
            
            if (UserManager.FindByName(AppConfig.defaultUser.UserName) == null) {
                var user = new ApplicationUser { UserName = AppConfig.defaultUser.UserName, Email = AppConfig.defaultUser.Email };
                var chkUser = UserManager.Create(user, AppConfig.defaultUser.Password);
                if (chkUser.Succeeded) {
                    var result1 = UserManager.AddToRole(user.Id, AppConfig.defaultUser.Role);
                }
            }
        }
    }
}
