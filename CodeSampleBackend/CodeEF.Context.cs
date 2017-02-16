﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeSampleBackend
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MoonCakeCodeSampleEntities : DbContext
    {
        public MoonCakeCodeSampleEntities()
            : base("name=MoonCakeCodeSampleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<CodeOwnership> CodeOwnerships { get; set; }
        public virtual DbSet<Commit> Commits { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<ProcessLog> ProcessLogs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SupportEngineer> SupportEngineers { get; set; }
        public virtual DbSet<UTLog> UTLogs { get; set; }
        public virtual DbSet<IssueStatu> IssueStatus { get; set; }
        public virtual DbSet<IssueStatusLog> IssueStatusLogs { get; set; }
    }
}
