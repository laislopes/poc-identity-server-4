using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProtectedApi.Config;

namespace ProtectedApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                    .AddAuthorization(GetAuthorizationConfig())
                    .AddAuthentication(GetAuthenticationConfig())
                    .AddJwtBearer(GetJwtConfig());
            services.AddControllers();
        }

        private static Action<AuthenticationOptions> GetAuthenticationConfig()
        {
            return options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }
        private static Action<AuthorizationOptions> GetAuthorizationConfig()
        {
            return options =>
            {

                options
                    .AddPolicy("AuthorizationPolicy",
                                policy =>
                                {
                                    policy.AddRequirements(new MyRequirement());
                                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                                    //policy.RequireClaim("scope", "api1");
                                });
            };
        }

        private static Action<JwtBearerOptions> GetJwtConfig()
        {
            return jwtBearerOptions =>
            {
                jwtBearerOptions.Authority = "https://localhost:5001";
                jwtBearerOptions.Audience = "api1";
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        return new JwtSecurityToken(token);
                    }
                };
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
