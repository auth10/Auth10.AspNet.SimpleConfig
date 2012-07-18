using System.Web;
using Microsoft.IdentityModel.Web;

namespace Auth10.FederatedIdentity.Infrastructure
{
    public class CustomSessionAuthenticationModule : SessionAuthenticationModule
    {
        protected override void InitializeModule(HttpApplication context)
        {
            // shortcircuit registration of the module SessionAuthenticationModule events if fed auth is not configured
            var settings = new FederatedIdentityConfiguration();
            if (!string.IsNullOrEmpty(settings.IdentityProviderUrl))
            {
                base.InitializeModule(context);
            }
        }

        protected override void InitializePropertiesFromConfiguration(string serviceName)
        {
            var settings = new FederatedIdentityConfiguration();

            this.CookieHandler.RequireSsl = settings.RequiresSsl;
        }
    }
}