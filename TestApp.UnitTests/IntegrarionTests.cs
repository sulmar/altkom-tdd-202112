using Ductus.FluentDocker.Services;
using NUnit.Framework;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.UnitTests
{
    // Install-Package Ductus.FluentDocker
    public class IntegrationTests
    {
        private IContainerService container;
        
        [SetUp]
        public void SetUp()
        {
            container = new Ductus.FluentDocker.Builders.Builder()
             .UseContainer()
             .WithHostName("localhost")
             .UseImage("redis")             
             .ExposePort(6379, 6379)
             .WaitForPort("6379/tcp", TimeSpan.FromSeconds(30))
             .Build()
             .Start();
        }

        [Test]
        public void Test()
        {
            IConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost");

            IDatabase database = connection.GetDatabase();

            database.Ping();
        }

        [TearDown]
        public void TearDown()
        {
            container.Dispose();
        }
    }
}
