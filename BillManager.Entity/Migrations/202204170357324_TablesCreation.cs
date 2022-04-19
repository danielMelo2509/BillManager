namespace BillManager.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        IdBill = c.Int(nullable: false, identity: true),
                        BillNumber = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PaymentType = c.String(),
                        CustomerDocument = c.Long(nullable: false),
                        CustomerName = c.String(),
                        Subtotal = c.Long(nullable: false),
                        Discount = c.Int(nullable: false),
                        IVA = c.Int(nullable: false),
                        TotalDiscount = c.Double(nullable: false),
                        TotalTax = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdBill);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        IdDetail = c.Int(nullable: false, identity: true),
                        IdBill = c.Int(nullable: false),
                        IdProduct = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDetail)
                .ForeignKey("dbo.Bills", t => t.IdBill, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.IdProduct, cascadeDelete: true)
                .Index(t => t.IdBill)
                .Index(t => t.IdProduct);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        IdProduct = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.IdProduct);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Details", "IdProduct", "dbo.Products");
            DropForeignKey("dbo.Details", "IdBill", "dbo.Bills");
            DropIndex("dbo.Details", new[] { "IdProduct" });
            DropIndex("dbo.Details", new[] { "IdBill" });
            DropTable("dbo.Products");
            DropTable("dbo.Details");
            DropTable("dbo.Bills");
        }
    }
}
