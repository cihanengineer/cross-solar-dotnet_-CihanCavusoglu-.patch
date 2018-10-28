using CrossSolar.Exceptions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CrossSolar.Exceptions;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CrossSolar.Tests.Controller
{
    public class ExceptionControllerTests
    {
        private readonly ILogger<HttpStatusCodeExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;


        public ExceptionControllerTests()
        {

        }

        [Fact]
        public void TestHttpStatusCodeException()
        {
            var ex = new HttpStatusCodeException(200);
            Assert.NotNull(ex);


        }

        [Fact]
        public void TestHttpStatusCodeMessageException()
        {
            var ex = new HttpStatusCodeException(200,"Success");
            Assert.NotNull(ex);


        }

        [Fact]
        public void TestHttpException()
        {
            var ex = new HttpStatusCodeException(200,new Exception());
            Assert.NotNull(ex);


        }

        [Fact]
        public void TestMiddleware()
        {
            var builder = new Mock<IApplicationBuilder>().Object;
            var build= builder.UseHttpStatusCodeExceptionMiddleware();

            Assert.NotNull(build);
           // Assert.NotNull(builder.UseMiddleware());
        }

    }
}
