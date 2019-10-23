using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLab.Oas.ObjectModel;
using Xunit;

namespace MyLab.Oas.Tests
{
    public class ApiDataContractBehavior
    {
        [Fact]
        public void ShouldDetectObject()
        {
            //Arrange
            var objContract = TestTools.ApiDesc.DataContracts.Single(c => c.Id == "ResultValue");

            //Act


            //Assert
            Assert.Equal(ContractType.Object, objContract.Type);
            Assert.Contains(objContract.Properties, p => p.Name == "val");
        }

        [Fact]
        public void ShouldBindReferences()
        {
            //Arrange
            var contract = TestTools.ApiDesc.DataContracts.Single(c => c.Id == "GetResult");
            var refContract = contract.Properties.FirstOrDefault(p => p.Name == "value");

            //Act


            //Assert
            Assert.NotNull(refContract);
            Assert.Equal("ResultValue", refContract.Contract.Id);
        }

        [Theory]
        [InlineData("item1", "description1")]
        [InlineData("item2", "description2")]
        public void ShouldDetectEnums(string itemValue, string expectedDescription)
        {
            //Arrange
            var enumContract = TestTools.ApiDesc.DataContracts.Single(c => c.Id == "EnumContract");
            var item = enumContract.EnumValues?.FirstOrDefault(e => e.Value == itemValue);

            //Act


            //Assert
            Assert.NotNull(item);
            Assert.Equal(expectedDescription, item.Comment);
        }

        [Fact]
        public void ShouldDetectArray()
        {
            //Arrange
            var arrayContract = TestTools.ApiDesc.DataContracts.Single(c => c.Id == "ArrayContract");

            //Act


            //Assert
            Assert.Equal(ContractType.Array, arrayContract.Type);
            Assert.Equal(ContractType.Integer, arrayContract.ItemsContract.Type);
        }
    }
}
