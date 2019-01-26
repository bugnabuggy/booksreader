using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Infrastructure.Configuration;

namespace BooksReader.TestData.Helpers
{
    class TestDbContextInitializer
    {
        private static bool _isInitialized = false;
        private Random _random = new Random();

        public async Task SeedData(IServiceProvider services)
        {
            if (TestDbContextInitializer._isInitialized)
            {
                return;
            }

            TestDbContextInitializer._isInitialized = true;

            // because we have service permission checks, principal should be an admin

            
            AppConfigurator.InitRolesAndUsers(services);

            //_context = services.GetService<N2NDataContext>();
            //var apiUserSrv = services.GetService<N2NApiUserService>();

            //var _userManager = services.GetService<UserManager<N2NIdentityUser>>();
            //await _userManager.CreateAsync(N2N.TestData.N2NUsersList.GetN2NIdentityUser(), HardCoddedConfig.DefaultPassword);

            ////create users, create promises for users
            //_users = N2NUsersList.GetList().ToArray();

            //foreach (var user in _users)
            //{
            //    var result = await apiUserSrv.CreateUserAsync(user, HardCoddedConfig.DefaultPassword, new[] { N2NRoles.User });
            //    if (!result.Success)
            //    {
            //        throw new Exception(string.Concat(result.Messages));
            //    }
            //}

            ////to avoid foreing keys insert conflicts
            //foreach (var user in _users)
            //{
            //    AddPromises(user, _context);
            //    AddPostcards(user, _context);
            //    AddAddressess(user, _context);
            //}
        }
    }
}
