using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FreeCourse.Web.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContextAccessor _httpcontextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, HttpContextAccessor httpcontextAccessor,
            IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpcontextAccessor = httpcontextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> SignIn(SignInInput signinInput)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecrets,
                UserName = signinInput.Email,
                Password = signinInput.Password,
                Address = disco.TokenEndpoint
            };

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if(token.IsError)
            {
                var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();

                var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Response<bool>.Fail(errorDto.Errors, 400);
            }

            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = disco.UserInfoEndpoint
            };
            var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError)
            {
                throw userInfo.Exception;
            }

            //TODO: CLAIMS AND COOKIE
        }
    }
}
