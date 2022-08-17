using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Models;

namespace Client;

public class Adb2cAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public Adb2cAuthStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsIdentity = new ClaimsIdentity();

        var response = await _httpClient.GetFromJsonAsync<ClientPrincipalPayLoad>("/.auth/me");

        if (response is not null)
        {
            if (response.ClientPrincipal is not null)
            {
                claimsIdentity = new ClaimsIdentity(response.ClientPrincipal.IdentityProvider);

                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, response.ClientPrincipal.UserDetails));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, response.ClientPrincipal.UserId));

                claimsIdentity.AddClaims(response.ClientPrincipal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
            }
        }

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authenticationState = new AuthenticationState(claimsPrincipal);

        return authenticationState;
    }    

    private class ClientPrincipalPayLoad
    {
        public ClientPrincipal? ClientPrincipal { get; set; }
    }

}

