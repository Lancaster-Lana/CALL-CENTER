namespace Laneta.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address_Street = c.String(),
                        Address_City = c.String(),
                        Address_State = c.String(),
                        Address_Zip = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address_Street = c.String(),
                        Address_City = c.String(),
                        Address_State = c.String(),
                        Address_Zip = c.String(),
                        Identity = c.String(),
                        ServiceAreas = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sent = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScheduleItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        ServiceTicketID = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        WorkHours = c.Int(nullable: false),
                        AssignedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceTickets", t => t.ServiceTicketID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.ServiceTicketID);
            
            CreateTable(
                "dbo.ServiceTickets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        StatusValue = c.Int(nullable: false),
                        EscalationLevel = c.Int(nullable: false),
                        Opened = c.DateTime(),
                        Closed = c.DateTime(),
                        CustomerID = c.Int(),
                        CreatedByID = c.Int(),
                        AssignedToID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.AssignedToID)
                .ForeignKey("dbo.Employees", t => t.CreatedByID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.CustomerID)
                .Index(t => t.CreatedByID)
                .Index(t => t.AssignedToID);
            
            CreateTable(
                "dbo.ServiceLogEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Description = c.String(),
                        CreatedByID = c.Int(),
                        ServiceTicketID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.CreatedByID)
                .ForeignKey("dbo.ServiceTickets", t => t.ServiceTicketID, cascadeDelete: true)
                .Index(t => t.CreatedByID)
                .Index(t => t.ServiceTicketID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleItems", "ServiceTicketID", "dbo.ServiceTickets");
            DropForeignKey("dbo.ServiceLogEntries", "ServiceTicketID", "dbo.ServiceTickets");
            DropForeignKey("dbo.ServiceLogEntries", "CreatedByID", "dbo.Employees");
            DropForeignKey("dbo.ServiceTickets", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.ServiceTickets", "CreatedByID", "dbo.Employees");
            DropForeignKey("dbo.ServiceTickets", "AssignedToID", "dbo.Employees");
            DropForeignKey("dbo.ScheduleItems", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.ServiceLogEntries", new[] { "ServiceTicketID" });
            DropIndex("dbo.ServiceLogEntries", new[] { "CreatedByID" });
            DropIndex("dbo.ServiceTickets", new[] { "AssignedToID" });
            DropIndex("dbo.ServiceTickets", new[] { "CreatedByID" });
            DropIndex("dbo.ServiceTickets", new[] { "CustomerID" });
            DropIndex("dbo.ScheduleItems", new[] { "ServiceTicketID" });
            DropIndex("dbo.ScheduleItems", new[] { "EmployeeID" });
            DropTable("dbo.ServiceLogEntries");
            DropTable("dbo.ServiceTickets");
            DropTable("dbo.ScheduleItems");
            DropTable("dbo.Messages");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.Alerts");
        }
    }
}
