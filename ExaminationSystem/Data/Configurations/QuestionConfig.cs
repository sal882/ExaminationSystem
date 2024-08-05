using ExaminationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Data.Configurations
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.Text).IsRequired();

            builder.Property(q => q.Level)
                .HasConversion(
                    level => level.ToString(),
                    level => (QuestionLevel)Enum.Parse(typeof(QuestionLevel), level)
                );
        }
    }
    
}
