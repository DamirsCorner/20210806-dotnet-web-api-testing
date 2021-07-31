using NUnit.Framework;
using System.Threading.Tasks;

namespace WebApiTesting.Tests
{
    public class ConfigTests : WebApiTestBase
    {
        protected override string ApiRoute => "config/";

        [Test]
        public async Task GetConfig()
        {
            var value = await this.Client.GetStringAsync("Key1");

            Assert.That(value, Is.EqualTo("Value 1"));
        }
    }
}
