using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmprestimoLivros.Migrations
{
    /// <inheritdoc />
    public partial class RenameLoanToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Emprestimos",
                newName: "Loans");

            migrationBuilder.RenameColumn(
                name: "Recebedor",
                table: "Loans",
                newName: "Borrower");

            migrationBuilder.RenameColumn(
                name: "Fornecedor",
                table: "Loans",
                newName: "Lender");

            migrationBuilder.RenameColumn(
                name: "LivroEmprestado",
                table: "Loans",
                newName: "BookTitle");

            migrationBuilder.RenameColumn(
                name: "DataUltimaAtualizacao",
                table: "Loans",
                newName: "LastUpdatedAt");

            migrationBuilder.Sql(
                "ALTER TABLE \"Loans\" RENAME CONSTRAINT \"PK_Emprestimos\" TO \"PK_Loans\";");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Loans\" RENAME CONSTRAINT \"PK_Loans\" TO \"PK_Emprestimos\";");

            migrationBuilder.RenameColumn(
                name: "Borrower",
                table: "Loans",
                newName: "Recebedor");

            migrationBuilder.RenameColumn(
                name: "Lender",
                table: "Loans",
                newName: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "BookTitle",
                table: "Loans",
                newName: "LivroEmprestado");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Loans",
                newName: "DataUltimaAtualizacao");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "Emprestimos");
        }
    }
}
