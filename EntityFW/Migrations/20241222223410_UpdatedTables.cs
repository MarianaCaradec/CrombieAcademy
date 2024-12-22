using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFW.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_ObrasSociales_ObraSocialId",
                table: "Paciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historiales",
                table: "Historiales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Historiales");

            migrationBuilder.RenameTable(
                name: "Paciente",
                newName: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "Turnos",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "IdMedico",
                table: "Turnos",
                newName: "MedicoId");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "Historiales",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "IdMedico",
                table: "Historiales",
                newName: "MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_Paciente_ObraSocialId",
                table: "Pacientes",
                newName: "IX_Pacientes_ObraSocialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historiales",
                table: "Historiales",
                columns: new[] { "MedicoId", "PacienteId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_MedicoId",
                table: "Turnos",
                column: "MedicoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ObraSocialId",
                table: "Turnos",
                column: "ObraSocialId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_PacienteId",
                table: "Turnos",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historiales_PacienteId",
                table: "Historiales",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Historiales_Medicos_MedicoId",
                table: "Historiales",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiales_Pacientes_PacienteId",
                table: "Historiales",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_ObrasSociales_ObraSocialId",
                table: "Pacientes",
                column: "ObraSocialId",
                principalTable: "ObrasSociales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Medicos_MedicoId",
                table: "Turnos",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_ObrasSociales_ObraSocialId",
                table: "Turnos",
                column: "ObraSocialId",
                principalTable: "ObrasSociales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Pacientes_PacienteId",
                table: "Turnos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historiales_Medicos_MedicoId",
                table: "Historiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiales_Pacientes_PacienteId",
                table: "Historiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_ObrasSociales_ObraSocialId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Medicos_MedicoId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_ObrasSociales_ObraSocialId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Pacientes_PacienteId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_MedicoId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_ObraSocialId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_PacienteId",
                table: "Turnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historiales",
                table: "Historiales");

            migrationBuilder.DropIndex(
                name: "IX_Historiales_PacienteId",
                table: "Historiales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                newName: "Paciente");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Turnos",
                newName: "IdPaciente");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "Turnos",
                newName: "IdMedico");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Historiales",
                newName: "IdPaciente");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "Historiales",
                newName: "IdMedico");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_ObraSocialId",
                table: "Paciente",
                newName: "IX_Paciente_ObraSocialId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Historiales",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historiales",
                table: "Historiales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_ObrasSociales_ObraSocialId",
                table: "Paciente",
                column: "ObraSocialId",
                principalTable: "ObrasSociales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
