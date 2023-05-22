using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class w : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bloods",
                columns: table => new
                {
                    b_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    h_id = table.Column<int>(type: "int", nullable: false),
                    b_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b_quantity = table.Column<int>(type: "int", nullable: false),
                    b_price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloods", x => x.b_Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    c_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.c_id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    e_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    e_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    e_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    e_centre_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.e_Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    h_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    h_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    h_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    h_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    h_loc_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    h_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    h_bloodquantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.h_Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    t_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    u_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_id = table.Column<int>(type: "int", nullable: false),
                    h_id = table.Column<int>(type: "int", nullable: false),
                    t_bloodtype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    t_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    t_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    t_bloodquantity = table.Column<int>(type: "int", nullable: false),
                    t_bloodprice = table.Column<int>(type: "int", nullable: false),
                    t_date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.t_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    u_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    u_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_bloodgroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_wallet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    u_status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.u_Id);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "c_id", "c_name" },
                values: new object[,]
                {
                    { 1, "Lahore" },
                    { 2, "Faisalabad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Bloods");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
