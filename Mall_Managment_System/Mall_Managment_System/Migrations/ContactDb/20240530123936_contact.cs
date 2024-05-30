using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mall_Managment_System.Migrations.ContactDb
{
    /// <inheritdoc />
    public partial class contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contact",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact_Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Contact_Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Contact_Number = table.Column<int>(type: "int", nullable: false),
                    Contact_Massage = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact");
        }
    }
}
