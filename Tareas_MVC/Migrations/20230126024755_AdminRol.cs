using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT Id from AspNetRoles WHERE Id='d1263021-2785-4de9-886e-e26e9f12c587')
                                    BEGIN
	                                    INSERT INTO AspNetRoles (Id,[Name],[NormalizedName])
	                                    VALUES ('d1263021-2785-4de9-886e-e26e9f12c587','admin','ADMIN')
                                    END
                                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AspNetRoles Where Id='d1263021-2785-4de9-886e-e26e9f12c587'");
        }
    }
}
