﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoShoppingWebsite.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbShoppingCarAzureEntities : DbContext
    {
        public dbShoppingCarAzureEntities()
            : base("name=dbShoppingCarAzureEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<table_Member> table_Member { get; set; }
        public virtual DbSet<table_Order> table_Order { get; set; }
        public virtual DbSet<table_OrderDetail> table_OrderDetail { get; set; }
        public virtual DbSet<table_Product> table_Product { get; set; }
    }
}
