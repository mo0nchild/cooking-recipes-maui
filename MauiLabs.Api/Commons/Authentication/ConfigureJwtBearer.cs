using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MauiLabs.Api.Commons.Authentication
{
    using JwtBearerConfig = ConfigureJwtBearer.JwtBearerConfig;
    public partial class ConfigureJwtBearer(IOptions<JwtBearerConfig> options) : IConfigureOptions<JwtBearerOptions>
    {
        protected JwtBearerConfig Configuration { get; private set; } = options.Value;
        protected byte[] SecurityKey { get => Encoding.UTF8.GetBytes(this.Configuration.SecretKey); }

        public sealed class JwtBearerConfig : object
        {
            public string Issuer { get; set; } = default!;
            public string Audience { get; set; } = default!;
            public string SecretKey { get; set; } = default!;
        }

        public virtual void Configure(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudience = this.Configuration.Audience,
                ValidateAudience = true,
                
                ValidIssuer = this.Configuration.Issuer,
                ValidateIssuer = true,

                IssuerSigningKey = new SymmetricSecurityKey(this.SecurityKey),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
            };
            options.SaveToken = true;
        }
    }
}
