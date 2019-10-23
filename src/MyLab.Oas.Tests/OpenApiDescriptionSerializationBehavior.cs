using Xunit;

namespace MyLab.Oas.Tests
{
    public class OpenApiDescriptionSerializationBehavior
    {
        [Fact]
        public void ShouldSerialize()
        {
            Assert.NotNull(TestTools.OasDesc);
        }
    }
}
