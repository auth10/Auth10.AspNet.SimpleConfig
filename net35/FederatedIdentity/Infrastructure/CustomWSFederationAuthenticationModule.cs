using System;
using System.IdentityModel.Selectors;
using System.Web;
using Microsoft.IdentityModel.Web;
using Auth10.FederatedIdentity;

namespace Auth10.FederatedIdentity.Infrastructure
{
    public partial class CustomWSFederationAuthenticationModule : WSFederationAuthenticationModule
    {
        private static bool initialized = false;
        private static object locker = new object();

        protected override void InitializeModule(HttpApplication context)
        {
            // shortcircuit registration of the module WSFederationAuthenticationModule events if fed auth is not configured
            var settings = new FederatedIdentityConfiguration();
            if (!string.IsNullOrEmpty(settings.IdentityProviderUrl))
            {
                base.InitializeModule(context);
            }
        }

        protected override void InitializePropertiesFromConfiguration(string serviceName)
        {
            var settings = new FederatedIdentityConfiguration();

            // do this once since FederatedAuthentication is singleton
            if (!initialized)
            {
                lock (locker)
                {
                    if (!initialized)
                    {
                        FederatedAuthentication.ServiceConfiguration.SecurityTokenHandlers.Configuration.IssuerNameRegistry = new CustomIssuerNameRegistry(settings.CertificateThumbprint);
                        FederatedAuthentication.ServiceConfiguration.AudienceRestriction.AllowedAudienceUris.Add(new Uri(settings.Realm));
                        FederatedAuthentication.ServiceConfiguration.SecurityTokenHandlers.Configuration.CertificateValidator = X509CertificateValidator.None;

                        initialized = true;
                    }
                }
            }
            
            // do this every time the module is used
            this.Realm = settings.Realm;
            this.Issuer = settings.IdentityProviderUrl;
            this.PassiveRedirectEnabled = !settings.ManualRedirectEnabled;
            this.RequireHttps = settings.RequiresSsl;
            this.Reply = settings.ReplyUrl;
        }
        
        //// hook to WIF pipeline events OnSignedIn, OnRedirectingToIdentityProvider, OnSessionSecurityTokenCreated, OnSignedOut, OnSignInError, OnSigningOut
		//// or do it grom Application_Start as regualr event handlers: RedirectingToIdentityProvider, SecurityTokenReceived, SecurityTokenValidated, SessionSecurityTokenCreated, SignedIn, SignedOut, SignInError, SigningOut, SignOutError, AuthorizationFailed        
        ////protected override void OnSignedIn(EventArgs args)
        ////{
        ////    base.OnSignedIn(args);
        ////}
    }
}