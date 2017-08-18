using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3_POC.Database;

namespace IdentityServer3_POC.Store
{
    public class ScopeStore : IScopeStore
    {
        public async Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            hPay_Demo_HSAEntities data = new hPay_Demo_HSAEntities();

            List<Scope> foundScopes = new List<Scope>();
            foreach (var scope in scopeNames)
            {
                var name = scope; 
                var dbScopeTask = data.IdentityServerScopes.FirstOrDefaultAsync(e => e.Name == name && e.Enabled == true);
                await dbScopeTask;

                var dbScope = dbScopeTask.Result;
                if (dbScope == null)
                {
                    continue;
                }

                Scope scopeToAdd = new Scope();
                scopeToAdd.Name = name;
                scopeToAdd.DisplayName = dbScope.DisplayName;
                
                // logic to create idsvr3 Scope model from db model
                foundScopes.Add(scopeToAdd);
            }
            foundScopes.AddRange(StandardScopes.All);
            return foundScopes;
        }

        public async Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            hPay_Demo_HSAEntities data = new hPay_Demo_HSAEntities();


            var scopes =
                from s in data.IdentityServerScopes.Include(x => x.Enabled == true)
                select s;

            List<Scope> foundScopes = new List<Scope>();

            foreach (var scope in scopes)
            {
                var name = scope; 
                var dbScopeTask = data.IdentityServerScopes.FirstOrDefaultAsync(e => e.Name == name.Name);
                await dbScopeTask;

                var dbScope = dbScopeTask.Result;
                if (dbScope == null)
                {
                    continue;
                }

                Scope scopeToAdd = new Scope();
                scopeToAdd.Name = name.Name;
                foundScopes.Add(scopeToAdd);
            }

            foundScopes.AddRange(StandardScopes.All);
            return foundScopes;
            // Very similar to above, but converting all db scopes to idsvr3 scopes
        }
    }
}