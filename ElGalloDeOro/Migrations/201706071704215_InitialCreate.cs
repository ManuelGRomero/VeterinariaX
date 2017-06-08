namespace ElGalloDeOro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archivos",
                c => new
                    {
                        archivoID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        tipo = c.String(),
                        formatoContenido = c.String(),
                        contenido = c.Binary(),
                        mascotaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.archivoID)
                .ForeignKey("dbo.Mascotas", t => t.mascotaID, cascadeDelete: true)
                .Index(t => t.mascotaID);
            
            CreateTable(
                "dbo.Mascotas",
                c => new
                    {
                        mascotaID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        raza = c.String(),
                        color = c.String(),
                        sexo = c.String(),
                        edad = c.String(),
                        vacuna = c.String(),
                        enfermded = c.String(),
                        fechaEsterilizacion = c.DateTime(),
                        Entrada = c.DateTime(nullable: false),
                        Salida = c.DateTime(),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.mascotaID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(),
                        apellidoP = c.String(),
                        apellidoM = c.String(),
                        fechaNac = c.DateTime(nullable: false),
                        telefono = c.String(),
                        domicilio = c.String(),
                        ciudad = c.String(),
                        rolid = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        citaID = c.Int(nullable: false, identity: true),
                        Estado = c.String(nullable: false),
                        MotivoCita = c.String(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        personaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.citaID)
                .ForeignKey("dbo.Personas", t => t.personaID, cascadeDelete: true)
                .Index(t => t.personaID);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        personaID = c.Int(nullable: false, identity: true),
                        nombres = c.String(nullable: false),
                        apellidoP = c.String(nullable: false),
                        apellidoM = c.String(nullable: false),
                        telefono = c.String(nullable: false),
                        direccion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.personaID);
            
            CreateTable(
                "dbo.MascotaClientes",
                c => new
                    {
                        mascotaID = c.Int(nullable: false, identity: true),
                        nombreMascota = c.String(nullable: false),
                        Raza = c.String(nullable: false),
                        sexo = c.String(nullable: false),
                        señasParticulares = c.String(nullable: false),
                        personaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.mascotaID)
                .ForeignKey("dbo.Personas", t => t.personaID, cascadeDelete: true)
                .Index(t => t.personaID);
            
            CreateTable(
                "dbo.Imagens",
                c => new
                    {
                        imagenID = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        tipo = c.String(),
                        formatoContenido = c.String(),
                        contenido = c.Binary(),
                        mascotaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.imagenID)
                .ForeignKey("dbo.MascotaClientes", t => t.mascotaID, cascadeDelete: true)
                .Index(t => t.mascotaID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MascotaClientes", "personaID", "dbo.Personas");
            DropForeignKey("dbo.Imagens", "mascotaID", "dbo.MascotaClientes");
            DropForeignKey("dbo.Citas", "personaID", "dbo.Personas");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Mascotas", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Archivos", "mascotaID", "dbo.Mascotas");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Imagens", new[] { "mascotaID" });
            DropIndex("dbo.MascotaClientes", new[] { "personaID" });
            DropIndex("dbo.Citas", new[] { "personaID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Mascotas", new[] { "Id" });
            DropIndex("dbo.Archivos", new[] { "mascotaID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Imagens");
            DropTable("dbo.MascotaClientes");
            DropTable("dbo.Personas");
            DropTable("dbo.Citas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Mascotas");
            DropTable("dbo.Archivos");
        }
    }
}
