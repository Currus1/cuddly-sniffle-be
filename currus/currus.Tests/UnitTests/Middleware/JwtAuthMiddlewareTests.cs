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

        private string CreateTokenString()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var credentials = new SigningCredentials(securityKey, "HS256");

            var header = new JwtHeader(credentials);

            JwtPayload payload = new JwtPayload();

            payload.AddClaim(new Claim("context", "{'user': { 'email': 'test@email.com' }}", JsonClaimValueTypes.Json));
            payload.AddClaim(new Claim("exp", DateTime.Now.AddHours(2).ToLongDateString()));
            var securityToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(securityToken);
        }

        [Test]
        public async Task JwtAuthMiddleware_Invoke_Success()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var credentials = new SigningCredentials(securityKey, "HS256");

            var header = new JwtHeader(credentials);

            JwtPayload payload = new JwtPayload();

            payload.AddClaim(new Claim("context", "{'user': { 'email': 'test@email.com' }}", JsonClaimValueTypes.Json));
            payload.AddClaim(new Claim("exp", DateTime.Now.AddHours(2).ToLongDateString()));
            var securityToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(securityToken);

            _httpContext.Request.Headers.Add("Authorization", $"Bearer {tokenString}");

            _middleWare = new JwtAuthMiddleware(next: (innerHttpContext) =>
            {
                return Task.CompletedTask;
            });

            await _middleWare.Invoke(_httpContext);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task JwtAuthMiddleware_Invoke_EmptyHeader()
        {
            _httpContext.Request.Headers.Remove("Authorization");

            _middleWare = new JwtAuthMiddleware(next: (innerHttpContext) =>
            {
                return Task.CompletedTask;
            });

            await _middleWare.Invoke(_httpContext);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task JwtAuthMiddleware_Invoke_InvalidHeader()
        {
            _httpContext.Request.Headers.Remove("Authorization");
            _httpContext.Request.Headers.Add("Authorization", $"Bearer");

            _middleWare = new JwtAuthMiddleware(next: (innerHttpContext) =>
            {
                return Task.CompletedTask;
            });

            await _middleWare.Invoke(_httpContext);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task JwtAuthMiddleware_Invoke_TimeExpired()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var credentials = new SigningCredentials(securityKey, "HS256");

            var header = new JwtHeader(credentials);

            JwtPayload payload = new JwtPayload();

            var expirationTime = DateTime.Now.Subtract(new DateTime(2020, 10, 10));

            payload.AddClaim(new Claim("context", "{'user': { 'email': 'test@email.com' }}", JsonClaimValueTypes.Json));
            payload.AddClaim(new Claim("exp", expirationTime.Seconds.ToString()));
            var securityToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(securityToken);

            _httpContext.Request.Headers.Remove("Authorization");
            _httpContext.Request.Headers.Add("Authorization", $"Bearer {tokenString}");

            _middleWare = new JwtAuthMiddleware(next: (innerHttpContext) =>
            {
                return Task.CompletedTask;
            });

            await _middleWare.Invoke(_httpContext);

            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(401));
        }
    }
}
