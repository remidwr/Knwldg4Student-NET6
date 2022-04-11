namespace Infrastructure.EntityConfigurations
{
    internal class RatingEntityTypeConfiguration
        : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> ratingConfiguration)
        {
            ratingConfiguration.ToTable("Ratings");

            ratingConfiguration.HasKey(r => r.Id);

            ratingConfiguration.Property(r => r.Comment);

            ratingConfiguration.Property(r => r.Stars)
                .HasColumnType("decimal(2, 1)")
                .HasDefaultValue(5)
                .IsRequired();

            ratingConfiguration.Property(r => r.RatedBy)
                .HasMaxLength(200)
                .IsRequired();

            ratingConfiguration.HasOne(r => r.Student)
                .WithMany(s => s.Ratings)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}