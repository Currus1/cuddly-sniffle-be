using currus.Controllers;
using currus.Middleware;
using currus.Tests.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace currus.Tests.UnitTests.Middleware
{
    internal class JwtAuthMiddlewareTests
    {
        private JwtAuthMiddleware _middleWare;
        private RequestDelegate _next;
        private DefaultHttpContext _httpContext;
        private string _key;

        [SetUp]
        public void SetUp()
        {
            _key = @"QWERTYQWERTYQWERTY";
            _next = async context => await context.Response.WriteAsync("Test Passed");
            _middleWare = new JwtAuthMiddleware(_next);

            _httpContext = new DefaultHttpContext();
            _httpContext.Request.Path = "/apisecure";
        }

        [Test]
        public async Task JwtAuthMiddleware_TestEmptyAuthHeader_ReturnStatusCodeUnauthorized()
        {
            _httpContext.Request.Headers.Authorization = "";

            await _middleWare.Invoke(_httpContext);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task JwtAuthMiddleware_TestInvalidAuthHeader_ReturnStatusCodeUnauthorized()
        {
            _httpContext.Request.Headers.Authorization = "Bearer token ABC";

            await _middleWare.Invoke(_httpContext);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));

        }

        [Test]
        public async Task JwtAuthMiddleware_NotValidAuthHeader_ReturnStatusCodeUnauthorized()
        {
            _httpContext.Request.Headers.Authorization = "Bearer abc.123.abc";

            await _middleWare.Invoke(_httpContext);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.Unauthorized));
        }
    }
}
