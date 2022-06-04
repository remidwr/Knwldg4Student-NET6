namespace Infrastructure.EntityConfigurations
{
    public class StudentEntityTypeConfiguration
        : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> studentConfiguration)
        {
            studentConfiguration.ToTable("Students");

            studentConfiguration.HasKey(s => s.Id);

            studentConfiguration.HasIndex(s => new { s.LastName, s.FirstName }, "IX_Students_FullName");

            studentConfiguration.HasIndex(s => s.Email, "UK_Students_Email")
                .IsUnique();

            studentConfiguration.Property(s => s.ExternalId)
                .IsRequired()
                .HasMaxLength(200);

            studentConfiguration.Property(s => s.Description);

            studentConfiguration.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(320);

            studentConfiguration.Property(s => s.FirstName)
                .HasMaxLength(50);

            studentConfiguration.Property(s => s.LastName)
                //.IsRequired()
                .HasMaxLength(50);

            studentConfiguration.Property(s => s.Username)
                .IsRequired()
                .HasMaxLength(50);

            studentConfiguration.HasMany(c => c.Courses)
                .WithMany(s => s.Students);
        }
    }
}