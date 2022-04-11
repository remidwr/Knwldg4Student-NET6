namespace Infrastructure.EntityConfigurations
{
    public class SectionEntityTypeConfiguration
        : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> sectionConfiguration)
        {
            sectionConfiguration.ToTable("Sections");

            sectionConfiguration.HasKey(s => s.Id);

            sectionConfiguration.HasIndex(s => s.Title, "UK_Sections_Title")
                .IsUnique();

            sectionConfiguration.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}