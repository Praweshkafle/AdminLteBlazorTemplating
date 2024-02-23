using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorTemplating.Layout.Manager
{
    public class CustomAuthManager : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthManager(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var key = await _localStorageService.GetItemAsync<string>("Authorized");

            if (string.IsNullOrEmpty(key))
            {
                return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(key)));
        }

        public void NotifyAuthState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
