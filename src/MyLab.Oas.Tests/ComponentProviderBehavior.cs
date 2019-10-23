using System;
using System.Collections.Generic;
using System.Text;
using MyLab.Oas.ObjectModel;
using Xunit;

namespace MyLab.Oas.Tests
{
    public class ComponentProviderBehavior
    {
        [Theory]
        [InlineData("Wrong base path", "#/components/parameters/Id", typeof(InvalidOperationException))]
        [InlineData("Null reference", null, typeof(ArgumentException))]
        [InlineData("Empty reference", "", typeof(ArgumentException))]
        [InlineData("Full file reference", "foo.yml", typeof(NotSupportedException))]
        [InlineData("Cross file reference", "foo.yml#/components/schemas/GetResult", typeof(NotSupportedException))]
        [InlineData("Wrong format reference", "#/compon###ents/schemas/GetResult", typeof(InvalidOperationException))]
        public void ShouldFailInvalidReferences(string desc, string reference, Type exceptionType)
        {
            //Arrange
            var cProvider = new ComponentProvider(TestTools.OasDesc.Components);

            //Act & Assert
            Assert.Throws(exceptionType, () => cProvider.ProvideSchema(reference));
        }

        [Fact]
        public void ShouldProvideSchema()
        {
            //Arrange
            var cProvider = new ComponentProvider(TestTools.OasDesc.Components);

            //Act
            var schema = cProvider.ProvideSchema("#/components/schemas/GetResult");

            //Assert
            Assert.NotNull(schema);
        }
    }
}
