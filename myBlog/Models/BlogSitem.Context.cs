﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class blogSitemEntities : DbContext
    {
        public blogSitemEntities()
            : base("name=blogSitemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Etiket> Etikets { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Makale> Makales { get; set; }
        public virtual DbSet<Mesaj> Mesajs { get; set; }
        public virtual DbSet<Resim> Resims { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Yorum> Yorums { get; set; }
    }
}
