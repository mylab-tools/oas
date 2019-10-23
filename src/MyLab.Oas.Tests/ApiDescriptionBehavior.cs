using Xunit;

namespace MyLab.Oas.Tests
{
    public class ApiDescriptionBehavior
    {
        [Fact]
        public void ShouldDetectServerMethods()
        {
            //Assert
            Assert.NotNull(TestTools.ApiDesc.ServerMethods);
            Assert.Contains(TestTools.ApiDesc.ServerMethods, m => m.Path == "/path-item/{id}" && m.Method == "POST");
            Assert.Contains(TestTools.ApiDesc.ServerMethods, m => m.Path == "/path-item/{id}" && m.Method == "GET");
        }
    }
}
