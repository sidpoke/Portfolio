using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMOCore.Repository;
using MMOCore.Repository.Models;

namespace MMOCore.EFRepository
{
    public class GameContext : DbContext
    {
        static GameContext()
        {
            Database.SetInitializer<GameContext>(new DropCreateDatabaseAlways<GameContext>());
        }

        public GameContext () : base ("name=GameDbContext") 
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<POCO_Session> Sessions { get; set; }
        public virtual DbSet<POCO_Event> Events { get;set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
