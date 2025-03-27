using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;

namespace OnlineLearning.Data.Seeds
{
    public class LanguageSeedConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasData(GetLanguages());
        }

        private static List<Language> GetLanguages()
        {
            return new List<Language>
            {
                new Language
                {
                    LanguageId = 1,
                    LanguageName = "English",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 2,
                    LanguageName = "Spanish",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 3,
                    LanguageName = "Mandarin Chinese",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 4,
                    LanguageName = "Hindi",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 5,
                    LanguageName = "Arabic",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 6,
                    LanguageName = "Bengali",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 7,
                    LanguageName = "Portuguese",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 8,
                    LanguageName = "Russian",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 9,
                    LanguageName = "Japanese",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 10,
                    LanguageName = "Vietnamese",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 11,
                    LanguageName = "Korean",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 12,
                    LanguageName = "French",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 13,
                    LanguageName = "German",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 14,
                    LanguageName = "Italian",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                },
                new Language
                {
                    LanguageId = 15,
                    LanguageName = "Turkish",
                    CreatedAt = DateTime.Now,
                    Status = CategoryStatus.Hided
                }
            };
        }
    }
}
