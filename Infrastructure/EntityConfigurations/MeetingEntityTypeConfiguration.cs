namespace Infrastructure.EntityConfigurations
{
    public class MeetingEntityTypeConfiguration
        : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> meetingConfiguration)
        {
            meetingConfiguration.ToTable("Meetings");

            meetingConfiguration.HasKey(m => m.Id);

            meetingConfiguration.Property(m => m.Description);

            meetingConfiguration.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(120);

            meetingConfiguration.HasOne(m => m.Course)
                .WithMany()
                .HasForeignKey(m => m.CourseId);

            meetingConfiguration.Property(m => m.MeetingStatusId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            meetingConfiguration.HasOne(m => m.MeetingStatus)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(m => m.MeetingStatusId);
        }
    }
}