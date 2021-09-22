using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public static class SigningCredentialsHelper
    {
        //Jwt sistemine al kardeşim bu senenin security key'in bu da kullanacağın algoritma demek için.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
