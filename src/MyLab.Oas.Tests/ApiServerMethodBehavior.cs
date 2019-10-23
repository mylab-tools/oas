using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MyLab.Oas.ObjectModel;
using Xunit;
using Xunit.Abstractions;

namespace MyLab.Oas.Tests
{
    public class ApiServerMethodBehavior
    {
        private readonly ITestOutputHelper _output;
        private readonly OpenApiServerMethod _post;
        private readonly OpenApiServerMethod _get;

        /// <summary>
        /// Initializes a new instance of <see cref="ApiServerMethodBehavior"/>
        /// </summary>
        public ApiServerMethodBehavior(ITestOutputHelper output)
        {
            _output = output;
            _post = TestTools.ApiDesc.ServerMethods.Single(m => m.Path == "/path-item/{id}" && m.Method == "POST");
            _get = TestTools.ApiDesc.ServerMethods.Single(m => m.Path == "/path-item/{id}" && m.Method == "GET");
        }

        [Fact]
        public void ShouldDetectService()
        {
            //Assert
            Assert.Equal("Tag1", _post.Service);
            Assert.Equal("Tag2", _get.Service);
        }

        [Fact]
        public void ShouldDetectDescription()
        {
            //Assert
            Assert.Equal("Method description", _post.Description);
            Assert.Null( _get.Description);
        }

        [Fact]
        public void ShouldDetectRequestBody()
        {
            //Assert
            Assert.NotNull(_post.RequestContents);
            Assert.Equal("application/json", _post.RequestContents[0].MimeType);
            Assert.Equal(ContractType.String, _post.RequestContents[0].Contract.Type);
        }

        [Fact]
        public void ShouldDetectSuccessResponseType()
        {
            //Assert
            Assert.NotNull(_get.ResponseContract);
            Assert.Equal("GetResult", _get.ResponseContract.Id);
        }

        [Fact]
        public void ShouldDetectImplicitSuccessResponse()
        {
            //Assert
            Assert.NotNull(_post.ResponseContract);
            Assert.Equal(ContractType.String, _post.ResponseContract.Type);
        }

        [Fact]
        public void ShouldDetectResponseDescriptions()
        {
            //Arrange
            var firstDescription = _post.ResponsesDescriptions?.FirstOrDefault();

            //Assert
            Assert.NotNull(firstDescription);
            
            _output.WriteLine(firstDescription.Code + " " + firstDescription.Comment);

            Assert.Equal(HttpStatusCode.NoContent, firstDescription.Code);
            Assert.Equal("Запрос создан", firstDescription.Comment);
        }
    }
}
