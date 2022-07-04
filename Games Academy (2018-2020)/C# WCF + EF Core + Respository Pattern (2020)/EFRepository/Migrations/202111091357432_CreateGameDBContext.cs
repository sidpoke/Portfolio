namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGameDBContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
            CreateTable(
                "dbo.Game Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);

            CreateStoredProcedure(
              "GetNextEvents",
              p => new
              {
                  ev = p.String()
              },
              @"SELECT Id, Name, Id + 1 as NextEvent 
                from dbo.[Game Events] 
                where Name = @ev"
              );

            CreateStoredProcedure(
              "GetLastEvents", @"SELECT SessionId, MAX(Id) as EventId from dbo.[Game Events] group by SessionId order by SessionId desc"
              );

        }
       
      //                @"SELECT Name, NextEvent, NextEventId
      //              from (
      //                  select
      //                  Name,
      //                  LEAD(Name) over (order by id) as NextEvent,
						//LEAD(Id) over (order by id) as NextEventId
      //                  from dbo.[Game Events]
      //              ) as t
      //              where Name = @ev"

        public override void Down()
        {
            DropForeignKey("dbo.Game Events", "SessionId", "dbo.Game Sessions");
            DropIndex("dbo.Game Events", new[] { "SessionId" });
            DropTable("dbo.Game Sessions");
            DropTable("dbo.Game Events");
            DropStoredProcedure("GetNextEvents");
        }
    }
}
