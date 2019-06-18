namespace mvcElectronix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        cartId = c.Int(nullable: false, identity: true),
                        amount = c.Int(nullable: false),
                        dataPurchased = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                        prodcutId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cartId)
                .ForeignKey("dbo.Products", t => t.prodcutId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.prodcutId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        dsecription = c.String(nullable: false),
                        image = c.String(nullable: false),
                        categoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.productId)
                .ForeignKey("dbo.Categories", t => t.categoryId, cascadeDelete: true)
                .Index(t => t.categoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.categoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 25),
                        age = c.Int(nullable: false),
                        password = c.String(nullable: false),
                        email = c.String(),
                        gender = c.String(nullable: false),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        roleId = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.roleId)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "userId", "dbo.Users");
            DropForeignKey("dbo.Roles", "userId", "dbo.Users");
            DropForeignKey("dbo.Carts", "prodcutId", "dbo.Products");
            DropForeignKey("dbo.Products", "categoryId", "dbo.Categories");
            DropIndex("dbo.Roles", new[] { "userId" });
            DropIndex("dbo.Products", new[] { "categoryId" });
            DropIndex("dbo.Carts", new[] { "prodcutId" });
            DropIndex("dbo.Carts", new[] { "userId" });
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
