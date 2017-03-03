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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
    
        public virtual DbSet<CodeOwnership> CodeOwnerships { get; set; }
        public virtual DbSet<Commit> Commits { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueStatu> IssueStatus { get; set; }
        public virtual DbSet<IssueStatusLog> IssueStatusLogs { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<ProcessLog> ProcessLogs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SupportEngineer> SupportEngineers { get; set; }
        public virtual DbSet<UTLog> UTLogs { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
    
        public virtual ObjectResult<getCodeView_Result> getCodeView(string alias, string process)
        {
            var aliasParameter = alias != null ?
                new ObjectParameter("alias", alias) :
                new ObjectParameter("alias", typeof(string));
    
            var processParameter = process != null ?
                new ObjectParameter("process", process) :
                new ObjectParameter("process", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCodeView_Result>("getCodeView", aliasParameter, processParameter);
        }
    
        public virtual ObjectResult<getIssueView_Result> getIssueView(string alias, string process)
        {
            var aliasParameter = alias != null ?
                new ObjectParameter("alias", alias) :
                new ObjectParameter("alias", typeof(string));
    
            var processParameter = process != null ?
                new ObjectParameter("process", process) :
                new ObjectParameter("process", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getIssueView_Result>("getIssueView", aliasParameter, processParameter);
        }
    }
}
