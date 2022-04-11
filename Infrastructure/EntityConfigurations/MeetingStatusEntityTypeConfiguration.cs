using Domain.AggregatesModel.MeetingAggregate;

namespace Infrastructure.EntityConfigurations
{
    public class MeetingStatusEntityTypeConfiguration
        : IEntityTypeConfiguration<MeetingStatus>
    {
        public void Configure(EntityTypeBuilder<MeetingStatus> meetingStatusConfiguration)
        {
            meetingStatusConfiguration.ToTable("MeetingStatus");

            meetingStatusConfiguration.HasKey(ms => ms.Id);

            meetingStatusConfiguration.Property(ms => ms.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            meetingStatusConfiguration.Property(ms => ms.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}