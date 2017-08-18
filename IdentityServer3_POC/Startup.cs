using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3_POC.Store;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdentityServer3_POC.Startup))]

namespace IdentityServer3_POC
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            var factory = new IdentityServerServiceFactory();
            factory.UseInMemoryUsers(new List<InMemoryUser>());

            var clientStore = new ClientStore();
            var scopeStore = new ScopeStore();


            factory.ClientStore = new Registration<IClientStore>(resolver => clientStore);
            factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStore);



            var options = new IdentityServerOptions
            {
                SiteName = "Embedded IdentityServer",
                Factory = factory
            };

            options.RequireSsl = false;

            app.UseIdentityServer(options);


            // accept access tokens from identityserver and require a scope of 'api1'
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:49921",
                ValidationMode = ValidationMode.ValidationEndpoint,

                RequiredScopes = new[] { "api" }
            });

            // configure web api
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // require authentication for all controllers
            config.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);





        }
    }
}
