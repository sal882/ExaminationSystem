using ExaminationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Data.Configurations
{
    public class ExamConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(q => q.Type)
                .HasConversion(
                    type => type.ToString(),
                    type => (ExamType)Enum.Parse(typeof(ExamType),type)
                );
        }
    }
}
