namespace IncludeTypeBackend.Services
{
    public class JwtService
    {
        private readonly string securityKey = "This is an extremely secure key!";

        public string Generate(string id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload(id, null, null, null, DateTime.Now.AddDays(1));
            var jwtSecurityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(securityKey);
            jwtSecurityTokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken securityToken);

            return (JwtSecurityToken)securityToken;
        }
    }
}
