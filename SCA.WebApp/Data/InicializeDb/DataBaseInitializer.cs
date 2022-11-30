using IdentityModel;
using Microsoft.AspNetCore.Identity;
using SCA.WebApp.IdentityConfiguration;
using System.Security.Claims;


namespace SCA.WebApp.Data.InicializeDb
{
   
        public class DataBaseInitializer : IDataBaseInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataBaseInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Responsável por criar os usuários de teste da aplicação
        public void InitializeRoles()
        {
            //Se o Perfil Admin não existir então cria 
            if (!_roleManager.RoleExistsAsync(Roles.Admin).Result)
            {                
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = Roles.Admin;
                roleAdmin.NormalizedName = Roles.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }

            // se o perfil Client não existir então cria
            if (!_roleManager.RoleExistsAsync(Roles.Client).Result)
            {                
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = Roles.Client;
                roleClient.NormalizedName = Roles.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();
            }
        }

        public void InitializeUsers()
        {
            //se o usuario admin não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("admin@com.br").Result == null)
            {
                //define os dados do usuário admin
                IdentityUser admin = new IdentityUser()
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@com.br",
                    NormalizedEmail = "ADMIN@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",                   
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Admin e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "WebAdm#2022").Result;
                if (resultAdmin.Succeeded)
                {
                    //inclui o usuário admin ao perfil admin
                    _userManager.AddToRoleAsync(admin, Roles.Admin).Wait();

                    //inclui as claims do usuário admin
                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, admin.UserName),
                    new Claim(JwtClaimTypes.Email, admin.Email),                    
                    new Claim(JwtClaimTypes.Role, Roles.Admin)
                    }).Result;
                }
            }

            //se o usuario client não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("engenharia1@com.br").Result == null)
            {
                //define os dados do usuário client
                IdentityUser client = new IdentityUser()
                {
                    UserName = "engenharia1",
                    NormalizedUserName = "ENGENHARIA1",
                    Email = "engenharia1@com.br",
                    NormalizedEmail = "ENGENHARIA1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",                   
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "WebEng#2022").Result;
                
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, Roles.Client).Wait();

                    //adiciona as claims do usuário Client
                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, client.UserName),
                    new Claim(JwtClaimTypes.Email, client.Email),                    
                    new Claim(JwtClaimTypes.Role, Roles.Client)
                    }).Result;
                }
            }
        }

        
    }
}
