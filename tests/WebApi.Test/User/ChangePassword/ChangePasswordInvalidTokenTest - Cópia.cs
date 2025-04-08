﻿using FluentAssertions;
using MyRecipeBook.Communication.Requests;
using System.Net;

namespace WebApi.Test.User.ChangePassword
{
    public class ChangePasswordInvalidTokenTest : MyRecipeBookClassFixture
    {
        private const string METHOD = "user/change-password";

        public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = new RequestChangePasswordJson();

            var response = await DoPut(METHOD, request, token: "tokenInvalid");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_Empty()
        {
            var request = new RequestChangePasswordJson();

            var response = await DoPut(METHOD, request, token: string.Empty);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}