using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.TestData.Helpers;
using BooksReader.Web.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using static Moq.It;

namespace BooksReader.Web.Tests
{
	[TestFixture]
	public class HubUsersTests
    {
        private static UserManager<BrUser> _userManager;

        //[OneTimeSetUp]
        //public void Start()
        //{
        //    var services = new DatabaseDiBootstrapperInMemory().GetServiceProvider();
        //    _userManager = services.GetService<UserManager<BrUser>>();
        //}
             

		[Test]
		public async Task ShouldSendHubStatistics()
        {
            var conn = TestConfig.GetConfig("connection");
            
            Console.WriteLine($"Configuration value [{conn}]");

            bool sendCalled = false;
            string calledMethod = "";
            object parameter;
            var hub = new UserHub(_userManager);

            var all = new Mock<IClientProxy>();
            all.Setup(x=>x.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .Callback<string, object, CancellationToken>((x, y, z) =>
                {
                    calledMethod = x;
                    parameter = y;
                })
                .Returns(() =>
                {
                    sendCalled = true;
                    return  Task.CompletedTask;
                } );

            
            var mockClients = new Mock<IHubCallerClients>();
            mockClients.Setup(m => m.All).Returns(() => all.Object);
            hub.Clients = mockClients.Object;
            
            await hub.CheckStatistics("any"); // ("TestUser", "TestMessage");
            Assert.True(sendCalled);
        }

	}
}
