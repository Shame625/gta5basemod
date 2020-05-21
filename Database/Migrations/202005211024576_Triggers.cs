namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Triggers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Triggers",
                c => new
                    {
                        TriggerId = c.Int(nullable: false, identity: true),
                        JobName = c.String(),
                        TriggerName = c.String(),
                        CronExpression = c.String(),
                    })
                .PrimaryKey(t => t.TriggerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Triggers");
        }
    }
}
