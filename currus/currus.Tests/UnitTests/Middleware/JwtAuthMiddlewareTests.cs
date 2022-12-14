using currus.Controllers;
using currus.Middleware;
using currus.Tests.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        [SetUp]
        public void SetUp()
        {
            _next = async context => await context.Response.WriteAsync("Test Passed");
            _middleWare = new JwtAuthMiddleware(_next);

            _httpContext = new DefaultHttpContext();
            _httpContext.Response.Body = new MemoryStream();
            _httpContext.Request.Path = "/apisecure";
        }

        [Test]
        public async Task JwtAuthMiddleware_Invoke_Success()
        {
            string key = @"secret-keyfgfgfggfgfgfgfgfgf";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(securityKey, "HS256");

            var header = new JwtHeader(credentials);

            JwtPayload payload = new JwtPayload();

            payload.AddClaim(new Claim("context", "{'user': { 'email': 'test1@email.com' }}", JsonClaimValueTypes.Json));
            payload.AddClaim(new Claim("exp", DateTime.Now.AddHours(2).ToLongDateString()));
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            const string expectedOutput = "Test Passed";

            _httpContext.Request.Headers.Add("Authorization", $"Bearer {tokenString}");
            //_httpContext.Items.Add("email", "test@email.com");

            _middleWare = new JwtAuthMiddleware(next: (innerHttpContext) =>
            {
                innerHttpContext.Response.WriteAsync(new StreamReader(innerHttpContext.Response.Body).ReadToEnd());
                return Task.CompletedTask;
            });

            await _middleWare.Invoke(_httpContext);

            //_httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            //var body = new StreamReader(_httpContext.Response.Body).ReadToEnd();

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public void JwtAuthMiddleware_Invoke_EmptyHeader()
        {
            var middleware = new JwtAuthMiddleware(_next);
        }

        [Test]
        public void JwtAuthMiddleware_Invoke_InvalidHeader()
        {
            var middleware = new JwtAuthMiddleware(_next);
        }

        [Test]
        public void JwtAuthMiddleware_Invoke_TokenNotExist()
        {
            var middleware = new JwtAuthMiddleware(_next);
        }
    }
}
