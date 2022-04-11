namespace Infrastructure.EntityConfigurations
{
    public class CourseEntityTypeConfiguration
        : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> courseConfiguration)
        {
            courseConfiguration.ToTable("Courses");

            courseConfiguration.HasKey(c => c.Id);

            courseConfiguration.HasIndex(c => c.Label, "UK_Courses_Label")
                .IsUnique();

            courseConfiguration.Property(c => c.Label)
                .IsRequired()
                .HasMaxLength(120);

            courseConfiguration.HasOne(c => c.Section)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Sections");

            courseConfiguration.HasMany(c => c.Students)
                .WithMany(s => s.Courses);
        }
    }
}