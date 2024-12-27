using Microsoft.EntityFrameworkCore;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void SeedAchievements(this ModelBuilder modelBuilder, string blobStorageUrl)
        {
            var baseAchievementUrl = $"{blobStorageUrl}/images/achievements";

            string AchievementUrl(string cause, string achievement) => $"{baseAchievementUrl}/{cause}/{achievement}.png";
            string CauseUrl(string cause) => $"{baseAchievementUrl}/{cause}/{cause}.png";

            var categories = new[]
            {
                new Cause { Name = "Meio ambiente", ImageUrl = CauseUrl("meio-ambiente") },
                new Cause { Name = "Educação", ImageUrl = CauseUrl("educacao") },
                new Cause { Name = "Proteção dos animais", ImageUrl = CauseUrl("protecao-dos-animais") },
                new Cause { Name = "Saúde", ImageUrl = CauseUrl("saude") },
                new Cause { Name = "Esporte", ImageUrl = CauseUrl("esporte") },
                new Cause { Name = "Doação", ImageUrl = CauseUrl("doacao-de-alimento") },
                new Cause { Name = "Apoio aos sem-teto", ImageUrl = CauseUrl("sem-teto") },
                new Cause { Name = "Moradia", ImageUrl = CauseUrl("moradia") },
                new Cause { Name = "Cultura", ImageUrl = CauseUrl("cultura") }
            };

            modelBuilder.Entity<Cause>().HasData(categories);

            var achievements = new List<Achievement>();

            void AddCauseAchievements(string causeName, int causeIndex, params (string Name, int TaskCount)[] achievementData)
            {
                foreach (var (name, taskCount) in achievementData)
                {
                    achievements.Add(new Achievement
                    {
                        Name = name,
                        TaskCount = taskCount,
                        ImageUrl = AchievementUrl(causeName, name.ToLower().Replace(" ", "-").Replace("ç", "c").Replace("ã", "a")),
                        CauseId = categories[causeIndex].Id
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

            // Specific achievements by cause
            AddCauseAchievements("meio-ambiente", 0,
                ("Eco-Guardião", 5),
                ("Protetor da Terra", 10),
                ("Herói Verde", 20),
                ("Guardião Ambiental", 50));

            AddCauseAchievements("educacao", 1,
                ("Tutor Comunitário", 5),
                ("Protetor do Conhecimento", 10),
                ("Herói do Futuro", 20),
                ("Guardião da Transformação", 50));

            AddCauseAchievements("protecao-dos-animais", 2,
                ("Anjo dos Animais", 5),
                ("Protetor da Fauna", 10),
                ("Herói das Patas", 20),
                ("Guardião dos Animais", 50));

            AddCauseAchievements("saude", 3,
                ("Anjo da Vida", 5),
                ("Protetor da Saúde", 10),
                ("Herói da Esperança", 20),
                ("Guardião da Vida", 50));

            AddCauseAchievements("esporte", 4,
                ("Atleta Solidário", 5),
                ("Protetor Esportivo", 10),
                ("Herói do Movimento", 20),
                ("Guardião das Medalhas", 50));

            AddCauseAchievements("doacao-de-alimento", 5,
                ("Provedor de Esperança", 5),
                ("Protetor da Esperança", 10),
                ("Herói da Nutrição", 20),
                ("Guardião da Fome Zero", 50));

            AddCauseAchievements("sem-teto", 6,
                ("Amigo de Rua", 5),
                ("Protetor da Esperança", 10),
                ("Herói Solidário", 20),
                ("Guardião da Dignidade", 50));

            AddCauseAchievements("moradia", 7,
                ("Construtor Solidário", 5),
                ("Protetor dos Lares", 10),
                ("Herói da Moradia", 20),
                ("Guardião dos Lares", 50));

            AddCauseAchievements("cultura", 8,
                ("Curador Comunitário", 5),
                ("Protetor da Tradição", 10),
                ("Herói Cultural", 20),
                ("Guardião das Artes", 50));

            modelBuilder.Entity<Achievement>().HasData(achievements);
        }
    }
}
