using ExaminationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Data.Configurations
{
    public class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasMany(i => i.Exams)
                .WithOne(e => e.Instructor)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
