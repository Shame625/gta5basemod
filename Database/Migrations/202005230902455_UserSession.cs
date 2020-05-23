namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 40),
                        SessionData = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sessions");
        }
    }
}
