using Xunit;

namespace MyLab.Oas.Tests
{
    public class ApiDescriptionSerializationBehavior
    {
        [Fact]
        public void ShouldSerialize()
        {
            Assert.NotNull(TestTools.OasDesc);
        }
    }
}
