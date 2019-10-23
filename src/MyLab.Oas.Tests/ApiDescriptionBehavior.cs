using Newtonsoft.Json.Serialization;
using Xunit;

namespace MyLab.Oas.Tests
{
    public class ApiDescriptionBehavior
    {
        [Theory]
        [InlineData("POST")]
        [InlineData("GET")]
        public void ShouldDetectServerMethods(string method)
        {
            //Assert
            Assert.Contains(TestTools.ApiDesc.ServerMethods, m => m.Path == "/path-item/{id}" && m.Method == method);
        }

        [Theory]
        [InlineData("GetResult")]
        [InlineData("ResultValue")]
        public void ShouldDetectDataContracts(string dcId)
        {
            //Assert
            Assert.Contains(TestTools.ApiDesc.DataContracts, dc => dc.Id == dcId);
        }
    }
}
