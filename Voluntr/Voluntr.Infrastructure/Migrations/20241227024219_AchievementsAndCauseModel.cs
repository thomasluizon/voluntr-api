using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Voluntr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AchievementsAndCauseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Address",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cause",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskCount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CauseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievement_Cause_CauseId",
                        column: x => x.CauseId,
                        principalTable: "Cause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCause",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CauseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCause", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCause_Cause_CauseId",
                        column: x => x.CauseId,
                        principalTable: "Cause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCause_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AchievementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAchievement_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievement_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Achievement",
                columns: new[] { "Id", "CauseId", "CreatedAt", "ImageUrl", "Name", "TaskCount", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("28c97415-30df-4051-b548-b0700c9f7e21"), null, new DateTime(2024, 12, 26, 23, 42, 19, 70, DateTimeKind.Unspecified).AddTicks(4646), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/fazedor-de-impacto.png", "Fazedor de Impacto", 10, null },
                    { new Guid("7761b02a-8668-47e0-8ca8-41ebc256fc3a"), null, new DateTime(2024, 12, 26, 23, 42, 19, 70, DateTimeKind.Unspecified).AddTicks(4663), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/heroi-da-comunidade.png", "Heroi da Comunidade", 50, null },
                    { new Guid("a82d2e1d-6baf-40ce-8e0e-216497d29d69"), null, new DateTime(2024, 12, 26, 23, 42, 19, 70, DateTimeKind.Unspecified).AddTicks(4656), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/agente-da-mudanca.png", "Agente de Mudança", 20, null },
                    { new Guid("b69311f5-1e5e-46ef-a85d-dd74933846fe"), null, new DateTime(2024, 12, 26, 23, 42, 19, 70, DateTimeKind.Unspecified).AddTicks(4116), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/arregacando-as-mangas.png", "Arregaçando as Mangas", 5, null }
                });

            migrationBuilder.InsertData(
                table: "Cause",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("39d0ad63-11ea-4f12-a249-ab09f18f61fa"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9128), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/protecao-dos-animais.png", "Proteção dos animais", null },
                    { new Guid("3b80c751-1fa7-4575-8ad3-b7eb13a47275"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9094), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/educacao.png", "Educação", null },
                    { new Guid("3ee43c2e-d3a5-4416-9689-ce40070f426d"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9168), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/doacao-de-alimento.png", "Doação", null },
                    { new Guid("66bdc4e9-97aa-49ee-9f27-845212d91640"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9174), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/sem-teto.png", "Apoio aos sem-teto", null },
                    { new Guid("68f9ba3b-bd75-4c2f-ae30-6939d823a573"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9145), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/saude.png", "Saúde", null },
                    { new Guid("7235ed34-4e0c-4e9f-98a0-ed100f0c19f7"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9179), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/moradia.png", "Moradia", null },
                    { new Guid("8f4cc0a4-9f79-4a13-9019-eba2392fa9cd"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9196), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/cultura.png", "Cultura", null },
                    { new Guid("a5e31dfb-2633-47dc-a81c-eb2215c632e8"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(9158), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/esporte.png", "Esporte", null },
                    { new Guid("b551d661-ff72-4477-812d-7c323bd25b1c"), new DateTime(2024, 12, 26, 23, 42, 19, 69, DateTimeKind.Unspecified).AddTicks(6832), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/meio-ambiente.png", "Meio ambiente", null }
                });

            migrationBuilder.InsertData(
                table: "Achievement",
                columns: new[] { "Id", "CauseId", "CreatedAt", "ImageUrl", "Name", "TaskCount", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("01d6468e-cb5c-499a-bbb8-cd20830d0d67"), new Guid("8f4cc0a4-9f79-4a13-9019-eba2392fa9cd"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3353), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/protetor-da-tradicao.png", "Protetor da Tradição", 10, null },
                    { new Guid("08813912-6305-4bd4-8b2f-415421833cbd"), new Guid("a5e31dfb-2633-47dc-a81c-eb2215c632e8"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3146), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/atleta-solidário.png", "Atleta Solidário", 5, null },
                    { new Guid("0ebb520d-8bdf-4cee-a7a0-b7b61b54e5e3"), new Guid("b551d661-ff72-4477-812d-7c323bd25b1c"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(2965), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/guardiao-ambiental.png", "Guardião Ambiental", 50, null },
                    { new Guid("10221e19-50f0-4b55-9263-44702f089265"), new Guid("66bdc4e9-97aa-49ee-9f27-845212d91640"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3284), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/protetor-da-esperanca.png", "Protetor da Esperança", 10, null },
                    { new Guid("48ee1c7a-4675-44d8-bf3a-a01c1b7ac986"), new Guid("39d0ad63-11ea-4f12-a249-ab09f18f61fa"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3077), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/protetor-da-fauna.png", "Protetor da Fauna", 10, null },
                    { new Guid("4ef7bf31-c1fe-4305-834c-a4d1cf403903"), new Guid("a5e31dfb-2633-47dc-a81c-eb2215c632e8"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3229), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/guardiao-das-medalhas.png", "Guardião das Medalhas", 50, null },
                    { new Guid("5a5de271-8916-472c-b2c4-a4cc6d639402"), new Guid("39d0ad63-11ea-4f12-a249-ab09f18f61fa"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3096), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/guardiao-dos-animais.png", "Guardião dos Animais", 50, null },
                    { new Guid("5aeb4147-e4e5-4bb8-8306-cf0718dccda1"), new Guid("3b80c751-1fa7-4575-8ad3-b7eb13a47275"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3048), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/herói-do-futuro.png", "Herói do Futuro", 20, null },
                    { new Guid("65f18693-f88c-4a8a-a5ed-5a111ee638f3"), new Guid("8f4cc0a4-9f79-4a13-9019-eba2392fa9cd"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3394), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/herói-cultural.png", "Herói Cultural", 20, null },
                    { new Guid("6d6290e9-5912-4bd9-99be-a8a2adbfea30"), new Guid("8f4cc0a4-9f79-4a13-9019-eba2392fa9cd"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3346), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/curador-comunitário.png", "Curador Comunitário", 5, null },
                    { new Guid("707787e0-7773-41f8-bfce-baa8a50669dd"), new Guid("3b80c751-1fa7-4575-8ad3-b7eb13a47275"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3039), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/protetor-do-conhecimento.png", "Protetor do Conhecimento", 10, null },
                    { new Guid("79e2997a-2e79-4400-adea-a14bb7c5e0de"), new Guid("3ee43c2e-d3a5-4416-9689-ce40070f426d"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3268), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/guardiao-da-fome-zero.png", "Guardião da Fome Zero", 50, null },
                    { new Guid("7f70a0c3-ae2e-4cef-95ca-0bee367b59d6"), new Guid("39d0ad63-11ea-4f12-a249-ab09f18f61fa"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3088), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/herói-das-patas.png", "Herói das Patas", 20, null },
                    { new Guid("8e979338-84ed-49a0-957e-97fb765da566"), new Guid("b551d661-ff72-4477-812d-7c323bd25b1c"), new DateTime(2024, 12, 26, 23, 42, 19, 70, DateTimeKind.Unspecified).AddTicks(7986), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/eco-guardiao.png", "Eco-Guardião", 5, null },
                    { new Guid("96c810b3-29f4-431a-9add-6ea47865700c"), new Guid("66bdc4e9-97aa-49ee-9f27-845212d91640"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3291), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/herói-solidário.png", "Herói Solidário", 20, null },
                    { new Guid("975c2ebe-3439-4d7f-a8ea-3418af3e3f21"), new Guid("7235ed34-4e0c-4e9f-98a0-ed100f0c19f7"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3338), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/guardiao-dos-lares.png", "Guardião dos Lares", 50, null },
                    { new Guid("98937f84-5dda-4f08-91a9-09bc66333846"), new Guid("66bdc4e9-97aa-49ee-9f27-845212d91640"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3304), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/guardiao-da-dignidade.png", "Guardião da Dignidade", 50, null },
                    { new Guid("9dec96c4-960a-4361-bc2d-3985d1c19f07"), new Guid("7235ed34-4e0c-4e9f-98a0-ed100f0c19f7"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3320), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/protetor-dos-lares.png", "Protetor dos Lares", 10, null },
                    { new Guid("a21ff111-af63-403b-86e8-1738f2ebbe77"), new Guid("7235ed34-4e0c-4e9f-98a0-ed100f0c19f7"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3332), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/herói-da-moradia.png", "Herói da Moradia", 20, null },
                    { new Guid("a2c7f7be-509f-4ff6-91a8-75536a6ccfed"), new Guid("a5e31dfb-2633-47dc-a81c-eb2215c632e8"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3168), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/herói-do-movimento.png", "Herói do Movimento", 20, null },
                    { new Guid("a74a5c9f-0151-4e45-bf20-f3a614ff7b4a"), new Guid("7235ed34-4e0c-4e9f-98a0-ed100f0c19f7"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3313), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/construtor-solidário.png", "Construtor Solidário", 5, null },
                    { new Guid("ac0dc5ec-ff0b-47d5-9621-5728afc8092d"), new Guid("68f9ba3b-bd75-4c2f-ae30-6939d823a573"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3133), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/guardiao-da-vida.png", "Guardião da Vida", 50, null },
                    { new Guid("b6fe59a4-e08b-4240-b46b-e71139ad5aac"), new Guid("8f4cc0a4-9f79-4a13-9019-eba2392fa9cd"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3402), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/guardiao-das-artes.png", "Guardião das Artes", 50, null },
                    { new Guid("c40a0035-3aec-4680-9ecd-4325ca0025ee"), new Guid("66bdc4e9-97aa-49ee-9f27-845212d91640"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3277), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/amigo-de-rua.png", "Amigo de Rua", 5, null },
                    { new Guid("ce1fe782-887f-468b-bfda-c7c6b0839c7e"), new Guid("68f9ba3b-bd75-4c2f-ae30-6939d823a573"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3105), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/anjo-da-vida.png", "Anjo da Vida", 5, null },
                    { new Guid("e0123a9c-60c2-4139-9e5f-c15fdd77f072"), new Guid("3ee43c2e-d3a5-4416-9689-ce40070f426d"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3245), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/provedor-de-esperanca.png", "Provedor de Esperança", 5, null },
                    { new Guid("e3ced851-597d-40b5-a143-35f1d85e4ec3"), new Guid("3ee43c2e-d3a5-4416-9689-ce40070f426d"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3253), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/protetor-da-esperanca.png", "Protetor da Esperança", 10, null },
                    { new Guid("e9c6c877-4e62-4cbd-9d2e-8352ca8d31c3"), new Guid("b551d661-ff72-4477-812d-7c323bd25b1c"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(2655), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/protetor-da-terra.png", "Protetor da Terra", 10, null },
                    { new Guid("e9f139f1-be49-4d91-b6ba-1c9fda3b62fb"), new Guid("3ee43c2e-d3a5-4416-9689-ce40070f426d"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3260), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/herói-da-nutricao.png", "Herói da Nutrição", 20, null },
                    { new Guid("eb422185-2a70-4d27-bf44-002f21c8d22b"), new Guid("a5e31dfb-2633-47dc-a81c-eb2215c632e8"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3160), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/protetor-esportivo.png", "Protetor Esportivo", 10, null },
                    { new Guid("eb7782e8-8851-40d4-83d3-072301814d31"), new Guid("3b80c751-1fa7-4575-8ad3-b7eb13a47275"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3028), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/tutor-comunitário.png", "Tutor Comunitário", 5, null },
                    { new Guid("ee38092d-544c-4bc4-8154-0fe1959b0398"), new Guid("39d0ad63-11ea-4f12-a249-ab09f18f61fa"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3071), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/anjo-dos-animais.png", "Anjo dos Animais", 5, null },
                    { new Guid("eea55ed8-32f9-437a-b69e-04a714fe036e"), new Guid("68f9ba3b-bd75-4c2f-ae30-6939d823a573"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3124), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/herói-da-esperanca.png", "Herói da Esperança", 20, null },
                    { new Guid("ef84ac94-4242-482b-834c-85f61fa4c4f7"), new Guid("3b80c751-1fa7-4575-8ad3-b7eb13a47275"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3060), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/guardiao-da-transformacao.png", "Guardião da Transformação", 50, null },
                    { new Guid("f62cb819-f6ba-4b7e-a443-db9ce01972af"), new Guid("b551d661-ff72-4477-812d-7c323bd25b1c"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(2893), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/herói-verde.png", "Herói Verde", 20, null },
                    { new Guid("fbfe8207-9804-4d67-a337-9d16e5d4442d"), new Guid("68f9ba3b-bd75-4c2f-ae30-6939d823a573"), new DateTime(2024, 12, 26, 23, 42, 19, 72, DateTimeKind.Unspecified).AddTicks(3112), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/protetor-da-saúde.png", "Protetor da Saúde", 10, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_CauseId",
                table: "Achievement",
                column: "CauseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievement_AchievementId",
                table: "UserAchievement",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievement_UserId",
                table: "UserAchievement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCause_CauseId",
                table: "UserCause",
                column: "CauseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCause_UserId",
                table: "UserCause",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address");

            migrationBuilder.DropTable(
                name: "UserAchievement");

            migrationBuilder.DropTable(
                name: "UserCause");

            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "Cause");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Address",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAddress_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_AddressId",
                table: "UserAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_UserId",
                table: "UserAddress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
