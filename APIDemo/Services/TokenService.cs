using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIDemo.Entities;
using APIDemo.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace APIDemo.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        // Read token key from configuration; throw if missing. (This is the shared secret used for HMAC signing.)
        var tokenKey = config["TokenKey"] ?? throw new Exception("Connot get token key"); // NOTE: original message has a typo "Connot" -> "Cannot".

        // Ensure the secret has sufficient length for cryptographic strength (recommended here >= 64 chars).
        if (tokenKey.Length < 64)
        {
            // If the secret is too short, throw an exception so the application won't run with a weak key.
            throw new Exception("Your token key needs to be >= 64 characters");
        }

        // Use symmetric encryption: convert the tokenKey string into raw bytes and create a SymmetricSecurityKey.
        // SymmetricSecurityKey is the key-holder object used by Microsoft.IdentityModel for HMAC signing.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Build the claims that will be embedded in the token payload (the "sub"/"claims" part of the JWT).
        // Claims are small pieces of identity data (email, id, roles, etc.).
        var claims = new List<Claim>
        {
            // Add the user's email as a claim. ClaimTypes.Email is a standardized claim type URI.
            new (ClaimTypes.Email, user.Email),

            // Add a unique identifier for the user. ClaimTypes.NameIdentifier is commonly used for user id.
            // Be careful: user.Id must be serializable to a string (Claim constructor expects string values).
            new (ClaimTypes.NameIdentifier, user.Id),
        };

        // Create signing credentials using the symmetric key and the HMAC-SHA512 algorithm.
        // SigningCredentials tells the token handler which key and algorithm to use to sign the token.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Describe the token: subject (claims identity), expiration, and signing credentials.
        // SecurityTokenDescriptor is a convenient container used by JwtSecurityTokenHandler to construct the token.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Subject is a ClaimsIdentity constructed from the claims list above.
            Subject = new ClaimsIdentity(claims),


            // Expiry: choose when the token will expire. Here it's set to UTC now + 7 days.
            // Important: consumers should always use UTC for token expirations to avoid timezone bugs.
            Expires = DateTime.UtcNow.AddDays(7),


            // Attach signing credentials so the token will be cryptographically signed.
            SigningCredentials = creds
        };

        // JwtSecurityTokenHandler is the class that creates and serializes JWTs.
        var tokenHandler = new JwtSecurityTokenHandler();

        // Create the security token from the descriptor. This returns a SecurityToken (signed JWT object).
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Serialize the SecurityToken to a compact JWT string (three base64url parts separated by dots).
        return tokenHandler.WriteToken(token);
    }
}
