using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorWebAssembly.Client;

public class ArrayClaimsPrincipalFactory<TAccount> : AccountClaimsPrincipalFactory<TAccount> where TAccount : RemoteUserAccount
{
    public ArrayClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor)
    : base(accessor)
    { }

    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(TAccount account, RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);
        var claimsIdentity = (ClaimsIdentity?)user.Identity;

        if (account != null)
        {
            foreach (var kvp in account.AdditionalProperties)
            {
                var name = kvp.Key;
                var value = kvp.Value;
                if (value != null && (value is JsonElement element && element.ValueKind == JsonValueKind.Array))
                {
                    claimsIdentity?.RemoveClaim(claimsIdentity.FindFirst(kvp.Key));

                    var claims = element.EnumerateArray()
                        .Select(x => new Claim(kvp.Key, x.ToString()));

                    claimsIdentity?.AddClaims(claims);
                }
            }
        }

        return user;
    }
}
