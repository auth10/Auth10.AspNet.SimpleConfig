using System;
using System.IdentityModel.Selectors;
using Microsoft.IdentityModel.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Auth10.FederatedIdentity.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Auth10.App_Start.FederatedIdentityBootstrapper), "PreAppStart")]

namespace Auth10.App_Start
{
    public static class FederatedIdentityBootstrapper
    {
        public static void PreAppStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(CustomWSFederationAuthenticationModule));
            DynamicModuleUtility.RegisterModule(typeof(CustomSessionAuthenticationModule));
        }
    }
}