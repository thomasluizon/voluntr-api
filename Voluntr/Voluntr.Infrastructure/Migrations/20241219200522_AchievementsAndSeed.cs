using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Voluntr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AchievementsAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievementCategory",
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
                    table.PrimaryKey("PK_AchievementCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskCount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievement_AchievementCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AchievementCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achievement_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
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
                columns: new[] { "Id", "CategoryId", "CreatedAt", "ImageUrl", "Name", "TaskCount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("3cc31b59-9765-4825-a3b4-a4de0e0b174b"), null, new DateTime(2024, 12, 19, 17, 5, 21, 976, DateTimeKind.Unspecified).AddTicks(2563), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/agente-da-mudanca.png", "Agente de Mudança", 20, null, null },
                    { new Guid("a3f3899a-43a4-4f83-a122-59df0a549087"), null, new DateTime(2024, 12, 19, 17, 5, 21, 976, DateTimeKind.Unspecified).AddTicks(2663), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/heroi-da-comunidade.png", "Heroi da Comunidade", 50, null, null },
                    { new Guid("b90e443d-9881-477a-a365-24b1096a426e"), null, new DateTime(2024, 12, 19, 17, 5, 21, 976, DateTimeKind.Unspecified).AddTicks(2552), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/fazedor-de-impacto.png", "Fazedor de Impacto", 10, null, null },
                    { new Guid("e5d0214c-53f0-4f18-8d51-2f590048413a"), null, new DateTime(2024, 12, 19, 17, 5, 21, 976, DateTimeKind.Unspecified).AddTicks(2003), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/arregacando-as-mangas.png", "Arregaçando as Mangas", 5, null, null }
                });

            migrationBuilder.InsertData(
                table: "AchievementCategory",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("09f32a7e-53e1-4ead-bb91-9227cad71adc"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7232), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/moradia.png", "Moradia", null },
                    { new Guid("0b2a0553-4f69-4419-a4d3-9266d6c1079e"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7211), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/esporte.png", "Esporte", null },
                    { new Guid("39488396-6021-490c-ab77-5c2a0b7faf01"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7188), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/protecao-dos-animais.png", "Proteção dos animais", null },
                    { new Guid("5bdd2754-befb-478d-a09d-4a78a93179be"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7238), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/cultura.png", "Cultura", null },
                    { new Guid("5d7f3b5a-403a-4ae5-811b-3fd9a07150d5"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7161), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/educacao.png", "Educação", null },
                    { new Guid("60ec27f0-45a0-47aa-a1f7-adf664858497"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7217), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/doacao-de-alimento.png", "Doação", null },
                    { new Guid("8c31424d-742d-4239-b49c-199303875ab4"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(4995), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/meio-ambiente.png", "Meio ambiente", null },
                    { new Guid("94d277c6-9899-4e2b-9a3e-409eae7efc62"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7221), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/sem-teto.png", "Apoio aos sem-teto", null },
                    { new Guid("d949ddf8-559e-4bdd-bd98-260b5b6ddba4"), new DateTime(2024, 12, 19, 17, 5, 21, 975, DateTimeKind.Unspecified).AddTicks(7199), null, "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/saude.png", "Saúde", null }
                });

            migrationBuilder.InsertData(
                table: "Achievement",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "ImageUrl", "Name", "TaskCount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("0c6b6bf3-6b87-479c-a046-9df88b188b52"), new Guid("60ec27f0-45a0-47aa-a1f7-adf664858497"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1335), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/guardiao-da-fome-zero.png", "Guardião da Fome Zero", 50, null, null },
                    { new Guid("0c9d51c0-1e83-4a65-ad91-725852266144"), new Guid("8c31424d-742d-4239-b49c-199303875ab4"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1043), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/guardiao-ambiental.png", "Guardião Ambiental", 50, null, null },
                    { new Guid("1154cf64-121c-464c-ad4d-260187e392ae"), new Guid("5bdd2754-befb-478d-a09d-4a78a93179be"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1438), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/herói-cultural.png", "Herói Cultural", 20, null, null },
                    { new Guid("12a24dc8-7427-4acc-b140-2eb5f7dcb733"), new Guid("09f32a7e-53e1-4ead-bb91-9227cad71adc"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1413), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/guardiao-dos-lares.png", "Guardião dos Lares", 50, null, null },
                    { new Guid("16b6c183-8ef1-4c00-ace9-920b6bf983c2"), new Guid("39488396-6021-490c-ab77-5c2a0b7faf01"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1154), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/protetor-da-fauna.png", "Protetor da Fauna", 10, null, null },
                    { new Guid("177fca7c-07a6-4e93-8eb8-f09a84becf26"), new Guid("94d277c6-9899-4e2b-9a3e-409eae7efc62"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1356), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/protetor-da-esperanca.png", "Protetor da Esperança", 10, null, null },
                    { new Guid("18767f7c-74c2-4935-b13d-5c5a4b744215"), new Guid("60ec27f0-45a0-47aa-a1f7-adf664858497"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1278), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/provedor-de-esperanca.png", "Provedor de Esperança", 5, null, null },
                    { new Guid("1b1ccb0c-5e67-4c57-bf0b-6ccb48da9985"), new Guid("8c31424d-742d-4239-b49c-199303875ab4"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(884), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/protetor-da-terra.png", "Protetor da Terra", 10, null, null },
                    { new Guid("230bc3d0-e762-4d37-bf7b-9def85662c54"), new Guid("94d277c6-9899-4e2b-9a3e-409eae7efc62"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1349), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/amigo-de-rua.png", "Amigo de Rua", 5, null, null },
                    { new Guid("23649e05-6470-46f1-abf3-a972e5beb695"), new Guid("39488396-6021-490c-ab77-5c2a0b7faf01"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1147), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/anjo-dos-animais.png", "Anjo dos Animais", 5, null, null },
                    { new Guid("2748ff31-5d7c-4805-afe7-5e12c5610487"), new Guid("5d7f3b5a-403a-4ae5-811b-3fd9a07150d5"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1135), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/guardiao-da-transformacao.png", "Guardião da Transformação", 50, null, null },
                    { new Guid("2c4e76b8-39d3-4063-a905-7e7e8a5b5362"), new Guid("d949ddf8-559e-4bdd-bd98-260b5b6ddba4"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1218), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/guardiao-da-vida.png", "Guardião da Vida", 50, null, null },
                    { new Guid("464e0a82-b5c2-44f1-bd44-39a67a808a3c"), new Guid("5d7f3b5a-403a-4ae5-811b-3fd9a07150d5"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1064), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/tutor-comunitário.png", "Tutor Comunitário", 5, null, null },
                    { new Guid("4c6d19f7-d2fc-471d-8c1a-7b505a271b22"), new Guid("5d7f3b5a-403a-4ae5-811b-3fd9a07150d5"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1077), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/protetor-do-conhecimento.png", "Protetor do Conhecimento", 10, null, null },
                    { new Guid("578aaa52-95ab-4f07-a0ba-012ed673a08c"), new Guid("39488396-6021-490c-ab77-5c2a0b7faf01"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1165), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/herói-das-patas.png", "Herói das Patas", 20, null, null },
                    { new Guid("579c8c70-6e44-4f00-a0e2-b1e97228ddb0"), new Guid("94d277c6-9899-4e2b-9a3e-409eae7efc62"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1371), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/guardiao-da-dignidade.png", "Guardião da Dignidade", 50, null, null },
                    { new Guid("5a393dbf-dbf4-4bb2-b2eb-0c75aa335ac5"), new Guid("5bdd2754-befb-478d-a09d-4a78a93179be"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1446), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/guardiao-das-artes.png", "Guardião das Artes", 50, null, null },
                    { new Guid("5f25990b-50db-4d2a-98d4-96aac9985f67"), new Guid("09f32a7e-53e1-4ead-bb91-9227cad71adc"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1382), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/construtor-solidário.png", "Construtor Solidário", 5, null, null },
                    { new Guid("64ee0c35-d9f3-471f-8f00-759d24a899de"), new Guid("5d7f3b5a-403a-4ae5-811b-3fd9a07150d5"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1126), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/educacao/herói-do-futuro.png", "Herói do Futuro", 20, null, null },
                    { new Guid("7566575b-352c-4695-abbb-38ccd46b58d1"), new Guid("0b2a0553-4f69-4419-a4d3-9266d6c1079e"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1226), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/atleta-solidário.png", "Atleta Solidário", 5, null, null },
                    { new Guid("795eeb7a-0a11-4cf2-a5c1-dc89f5c03943"), new Guid("94d277c6-9899-4e2b-9a3e-409eae7efc62"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1363), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/sem-teto/herói-solidário.png", "Herói Solidário", 20, null, null },
                    { new Guid("7bab8d31-cc36-4418-ab20-b49e964108eb"), new Guid("0b2a0553-4f69-4419-a4d3-9266d6c1079e"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1252), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/herói-do-movimento.png", "Herói do Movimento", 20, null, null },
                    { new Guid("8dd996b0-f044-40f0-9e26-fcb7058b3c55"), new Guid("5bdd2754-befb-478d-a09d-4a78a93179be"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1422), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/curador-comunitário.png", "Curador Comunitário", 5, null, null },
                    { new Guid("9dd50554-12b9-4c9c-a011-d0fff9575696"), new Guid("60ec27f0-45a0-47aa-a1f7-adf664858497"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1319), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/protetor-da-esperanca.png", "Protetor da Esperança", 10, null, null },
                    { new Guid("9e4242c5-525d-41a4-8b7f-7531479468bc"), new Guid("d949ddf8-559e-4bdd-bd98-260b5b6ddba4"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1200), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/protetor-da-saúde.png", "Protetor da Saúde", 10, null, null },
                    { new Guid("a2f236fd-11d4-4e92-ad7f-75a5e7ad937a"), new Guid("39488396-6021-490c-ab77-5c2a0b7faf01"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1180), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/protecao-dos-animais/guardiao-dos-animais.png", "Guardião dos Animais", 50, null, null },
                    { new Guid("b0957b9e-495c-4e89-80cc-5b89c287295d"), new Guid("09f32a7e-53e1-4ead-bb91-9227cad71adc"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1390), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/protetor-dos-lares.png", "Protetor dos Lares", 10, null, null },
                    { new Guid("b101c8d1-093d-4643-84ab-88725e393a20"), new Guid("d949ddf8-559e-4bdd-bd98-260b5b6ddba4"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1207), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/herói-da-esperanca.png", "Herói da Esperança", 20, null, null },
                    { new Guid("d3a04f7b-893f-48b5-a1a5-eacd22bf856b"), new Guid("0b2a0553-4f69-4419-a4d3-9266d6c1079e"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1260), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/guardiao-das-medalhas.png", "Guardião das Medalhas", 50, null, null },
                    { new Guid("d6475000-0596-4731-a72e-bdec9f9b4e2f"), new Guid("d949ddf8-559e-4bdd-bd98-260b5b6ddba4"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1192), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/saude/anjo-da-vida.png", "Anjo da Vida", 5, null, null },
                    { new Guid("d8982807-da0f-4bf8-83a5-f40d23ad5295"), new Guid("8c31424d-742d-4239-b49c-199303875ab4"), new DateTime(2024, 12, 19, 17, 5, 21, 976, DateTimeKind.Unspecified).AddTicks(5219), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/eco-guardiao.png", "Eco-Guardião", 5, null, null },
                    { new Guid("d953a37a-125f-4600-a530-0c4b98085b87"), new Guid("0b2a0553-4f69-4419-a4d3-9266d6c1079e"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1245), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/esporte/protetor-esportivo.png", "Protetor Esportivo", 10, null, null },
                    { new Guid("da280986-28f6-42fd-b4ec-3874043a4787"), new Guid("09f32a7e-53e1-4ead-bb91-9227cad71adc"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1406), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/moradia/herói-da-moradia.png", "Herói da Moradia", 20, null, null },
                    { new Guid("ebaeb1f7-181d-4fa1-a691-95a3b147ea88"), new Guid("60ec27f0-45a0-47aa-a1f7-adf664858497"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1326), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/doacao-de-alimento/herói-da-nutricao.png", "Herói da Nutrição", 20, null, null },
                    { new Guid("ebc60a7c-e387-4037-b870-02f204fd9d9c"), new Guid("5bdd2754-befb-478d-a09d-4a78a93179be"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(1430), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/cultura/protetor-da-tradicao.png", "Protetor da Tradição", 10, null, null },
                    { new Guid("fd7e941e-2a51-42ef-8b59-0af53d541848"), new Guid("8c31424d-742d-4239-b49c-199303875ab4"), new DateTime(2024, 12, 19, 17, 5, 21, 978, DateTimeKind.Unspecified).AddTicks(987), "https://voluntrprodeastusst.blob.core.windows.net/images/achievements/meio-ambiente/herói-verde.png", "Herói Verde", 20, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_CategoryId",
                table: "Achievement",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_UserId",
                table: "Achievement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievement_AchievementId",
                table: "UserAchievement",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievement_UserId",
                table: "UserAchievement",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAchievement");

            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "AchievementCategory");
        }
    }
}
