namespace Infrastructure.EntityConfigurations
{
    public class StudentMeetingEntityTypeConfiguration
        : IEntityTypeConfiguration<StudentMeeting>
    {
        public void Configure(EntityTypeBuilder<StudentMeeting> studentMeetingConfiguration)
        {
            studentMeetingConfiguration.ToTable("StudentMeetings");

            studentMeetingConfiguration.HasKey(sm => new { sm.StudentId, sm.MeetingId });

            studentMeetingConfiguration.HasOne(sm => sm.Meeting)
                .WithMany(m => m.StudentMeetings)
                .HasForeignKey(sm => sm.MeetingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentMeetings_Meetings");

            studentMeetingConfiguration.HasOne(sm => sm.Student)
                .WithMany(s => s.StudentMeetings)
                .HasForeignKey(sm => sm.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentMeetings_Students");
        }
    }
}
