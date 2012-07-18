using System;
using System.Configuration;

namespace Auth10.FederatedIdentity
{
    public class FederatedIdentityConfiguration
    {
        public string IdentityProviderUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["fedauth.identityProviderUrl"];
            }
        }

        public string Realm
        {
            get
            {
                return ConfigurationManager.AppSettings["fedauth.realm"];
            }
        }
        
        public string ReplyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["fedauth.replyUrl"];
            }
        }

        public string CertificateThumbprint
        {
            get
            {
                return ConfigurationManager.AppSettings["fedauth.certThumbprint"];
            }
        }

        public bool RequiresSsl
        {
            get
            {
                bool requireSsl = false;
                bool requireSslParsed = Boolean.TryParse(ConfigurationManager.AppSettings["fedauth.requireSsl"], out requireSsl);
                return requireSslParsed && requireSsl;
            }
        }

        public bool ManualRedirectEnabled
        {
            get
            {
                bool value = false;
                bool parsedValue = Boolean.TryParse(ConfigurationManager.AppSettings["fedauth.enableManualRedirect"], out value);
                return parsedValue && value;
            }
        }
    }
}