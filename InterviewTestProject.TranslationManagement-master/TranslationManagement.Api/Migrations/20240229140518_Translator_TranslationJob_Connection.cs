﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationManagement.Api.Migrations
{
    public partial class Translator_TranslationJob_Connection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Translators",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TranslationJobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TranslatorId",
                table: "TranslationJobs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslationJobs_TranslatorId",
                table: "TranslationJobs",
                column: "TranslatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslationJobs_Translators_TranslatorId",
                table: "TranslationJobs",
                column: "TranslatorId",
                principalTable: "Translators",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslationJobs_Translators_TranslatorId",
                table: "TranslationJobs");

            migrationBuilder.DropIndex(
                name: "IX_TranslationJobs_TranslatorId",
                table: "TranslationJobs");

            migrationBuilder.DropColumn(
                name: "TranslatorId",
                table: "TranslationJobs");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Translators",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TranslationJobs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
