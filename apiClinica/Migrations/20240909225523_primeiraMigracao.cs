using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiClinica.Migrations
{
    /// <inheritdoc />
    public partial class primeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanosSaude",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Cobertura = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosSaude", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PacientePlanos",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PlanoSaudeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientePlanos", x => new { x.PacienteId, x.PlanoSaudeId });
                    table.ForeignKey(
                        name: "FK_PacientePlanos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacientePlanos_PlanosSaude_PlanoSaudeId",
                        column: x => x.PlanoSaudeId,
                        principalTable: "PlanosSaude",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacientePlanos_PlanoSaudeId",
                table: "PacientePlanos",
                column: "PlanoSaudeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacientePlanos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "PlanosSaude");
        }
    }
}
