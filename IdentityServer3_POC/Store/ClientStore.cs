using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3_POC.Database;

namespace IdentityServer3_POC.Store
{
    public class ClientStore : IClientStore
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {

            hPay_Demo_HSAEntities db = new hPay_Demo_HSAEntities();

            var clientFromDb = db.IdentityClients.SingleOrDefault(x => x.Client_Id == clientId && x.Enabled == true);

            Client client = new Client();

            if (clientFromDb != null)
            {
                var clientSecretFromDb = db.IdentityClientSecrets.FirstOrDefault(x => x.Client_Id == clientFromDb.Id && x.Expiration > System.DateTime.Now);

                if (clientSecretFromDb != null)
                {
                    client.ClientId = clientFromDb.Client_Id;
                    client.ClientName = clientFromDb.Client_Name;
                    client.Enabled = clientFromDb.Enabled;
                    client.AccessTokenType = AccessTokenType.Reference;
                    client.Flow = Flows.ClientCredentials;
                    client.ClientSecrets = new List<Secret>
                    {
                        new Secret(clientSecretFromDb.Client_Secret.Sha256())
                    };
                    client.AllowedScopes = new List<string>
                    {
                        clientFromDb.Client_Scope
                    };
                    client.AccessTokenLifetime = clientFromDb.AccessTokenLifetime;
                }

            }

            return client;

        }
    }
}