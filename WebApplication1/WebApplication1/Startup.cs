using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IISDefaults.AuthenticationScheme;
                options.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                string authority = Configuration[ConfigKeys.AzureAd.Instance] + Configuration[ConfigKeys.AzureAd.TenantID];
                options.Authority = authority;
                options.ClientId = Configuration[ConfigKeys.AzureAd.ClientID];
                options.ClientSecret = Configuration[ConfigKeys.AzureAd.ClientSecret];
                options.CallbackPath = Configuration[ConfigKeys.AzureAd.CallbackPath];
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                //options.Resource = "https://graph.microsoft.com";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.UseTokenLifetime = true;
                options.RequireHttpsMetadata = true;
                options.SaveTokens = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authority,
                    NameClaimType = "name"
                };

                options.Events.OnRedirectToIdentityProvider += async (context) =>
                {
                    await Task.FromResult(0);
                };

                options.Events.OnAuthorizationCodeReceived += async (context) =>
                {
                    await Task.FromResult(0);
                };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews();
            services.Configure<IISServerOptions>(options => { options.AutomaticAuthentication = false; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseStaticFiles();
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
