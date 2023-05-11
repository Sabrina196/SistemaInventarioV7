using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaInventarioV7.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarInventarioDetalleMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventariosDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventarioId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    StockAnterior = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventariosDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventariosDetalles_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventariosDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventariosDetalles_InventarioId",
                table: "InventariosDetalles",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InventariosDetalles_ProductoId",
                table: "InventariosDetalles",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventariosDetalles");
        }
    }
}
