using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace Auth10.FederatedIdentity.Infrastructure
{
    public class CustomIssuerNameRegistry : IssuerNameRegistry
    {
        private readonly List<string> trustedThumbrpints = new List<string>();

        public CustomIssuerNameRegistry(string trustedThumbprint)
        {
            this.trustedThumbrpints.Add(trustedThumbprint);
        }

        public void AddTrustedIssuer(string thumbprint)
        {
            this.trustedThumbrpints.Add(thumbprint);
        }

        public override string GetIssuerName(System.IdentityModel.Tokens.SecurityToken securityToken)
        {
            var x509 = securityToken as X509SecurityToken;
            if (x509 != null)
            {
                foreach (string thumbprint in trustedThumbrpints)
                {
                    if (x509.Certificate.Thumbprint.Equals(thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        return x509.Certificate.Subject;
                    }
                }
            }

            return null;
        }
    }
}