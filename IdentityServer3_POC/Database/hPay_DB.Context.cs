﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdentityServer3_POC.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class hPay_Demo_HSAEntities : DbContext
    {
        public hPay_Demo_HSAEntities()
            : base("name=hPay_Demo_HSAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<IdentityClient> IdentityClients { get; set; }
        public virtual DbSet<IdentityClientSecret> IdentityClientSecrets { get; set; }
        public virtual DbSet<IdentityServerScope> IdentityServerScopes { get; set; }
    }
}
