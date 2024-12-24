using Microsoft.EntityFrameworkCore;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void SeedAchievements(this ModelBuilder modelBuilder, string blobStorageUrl)
        {
            var baseAchievementUrl = $"{blobStorageUrl}/images/achievements";

            string AchievementUrl(string category, string achievement) => $"{baseAchievementUrl}/{category}/{achievement}.png";
            string CategoryUrl(string category) => $"{baseAchievementUrl}/{category}/{category}.png";

            var categories = new[]
            {
                new AchievementCategory { Name = "Meio ambiente", ImageUrl = CategoryUrl("meio-ambiente") },
                new AchievementCategory { Name = "Educação", ImageUrl = CategoryUrl("educacao") },
                new AchievementCategory { Name = "Proteção dos animais", ImageUrl = CategoryUrl("protecao-dos-animais") },
                new AchievementCategory { Name = "Saúde", ImageUrl = CategoryUrl("saude") },
                new AchievementCategory { Name = "Esporte", ImageUrl = CategoryUrl("esporte") },
                new AchievementCategory { Name = "Doação", ImageUrl = CategoryUrl("doacao-de-alimento") },
                new AchievementCategory { Name = "Apoio aos sem-teto", ImageUrl = CategoryUrl("sem-teto") },
                new AchievementCategory { Name = "Moradia", ImageUrl = CategoryUrl("moradia") },
                new AchievementCategory { Name = "Cultura", ImageUrl = CategoryUrl("cultura") }
            };

            modelBuilder.Entity<AchievementCategory>().HasData(categories);

            var achievements = new List<Achievement>();

            void AddCategoryAchievements(string categoryName, int categoryIndex, params (string Name, int TaskCount)[] achievementData)
            {
                foreach (var (name, taskCount) in achievementData)
                {
                    achievements.Add(new Achievement
                    {
                        Name = name,
                        TaskCount = taskCount,
                        ImageUrl = AchievementUrl(categoryName, name.ToLower().Replace(" ", "-").Replace("ç", "c").Replace("ã", "a")),
                        CategoryId = categories[categoryIndex].Id
                    });
                }
            }

            achievements.AddRange(
            [
                new Achievement { Name = "Arregaçando as Mangas", TaskCount = 5, ImageUrl = $"{baseAchievementUrl}/arregacando-as-mangas.png" },
                new Achievement { Name = "Fazedor de Impacto", TaskCount = 10, ImageUrl = $"{baseAchievementUrl}/fazedor-de-impacto.png" },
                new Achievement { Name = "Agente de Mudança", TaskCount = 20, ImageUrl = $"{baseAchievementUrl}/agente-da-mudanca.png" },
                new Achievement { Name = "Heroi da Comunidade", TaskCount = 50, ImageUrl = $"{baseAchievementUrl}/heroi-da-comunidade.png" }
            ]);

            // Specific achievements by category
            AddCategoryAchievements("meio-ambiente", 0,
                ("Eco-Guardião", 5),
                ("Protetor da Terra", 10),
                ("Herói Verde", 20),
                ("Guardião Ambiental", 50));

            AddCategoryAchievements("educacao", 1,
                ("Tutor Comunitário", 5),
                ("Protetor do Conhecimento", 10),
                ("Herói do Futuro", 20),
                ("Guardião da Transformação", 50));

            AddCategoryAchievements("protecao-dos-animais", 2,
                ("Anjo dos Animais", 5),
                ("Protetor da Fauna", 10),
                ("Herói das Patas", 20),
                ("Guardião dos Animais", 50));

            AddCategoryAchievements("saude", 3,
                ("Anjo da Vida", 5),
                ("Protetor da Saúde", 10),
                ("Herói da Esperança", 20),
                ("Guardião da Vida", 50));

            AddCategoryAchievements("esporte", 4,
                ("Atleta Solidário", 5),
                ("Protetor Esportivo", 10),
                ("Herói do Movimento", 20),
                ("Guardião das Medalhas", 50));

            AddCategoryAchievements("doacao-de-alimento", 5,
                ("Provedor de Esperança", 5),
                ("Protetor da Esperança", 10),
                ("Herói da Nutrição", 20),
                ("Guardião da Fome Zero", 50));

            AddCategoryAchievements("sem-teto", 6,
                ("Amigo de Rua", 5),
                ("Protetor da Esperança", 10),
                ("Herói Solidário", 20),
                ("Guardião da Dignidade", 50));

            AddCategoryAchievements("moradia", 7,
                ("Construtor Solidário", 5),
                ("Protetor dos Lares", 10),
                ("Herói da Moradia", 20),
                ("Guardião dos Lares", 50));

            AddCategoryAchievements("cultura", 8,
                ("Curador Comunitário", 5),
                ("Protetor da Tradição", 10),
                ("Herói Cultural", 20),
                ("Guardião das Artes", 50));

            modelBuilder.Entity<Achievement>().HasData(achievements);
        }
    }
}
